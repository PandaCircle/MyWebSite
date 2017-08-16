using DeviceManageSite.Services;
using Orchard;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Mvc.Extensions;
using Orchard.Themes;
using Newtonsoft.Json;
using DeviceManageSite.ViewModels;

namespace DeviceManageSite.Controllers
{
    public class ClassifyController : Controller
    {
        private readonly IResourceManageService _resourceManageService;

        public ClassifyController(
            IOrchardServices orchardServices,
            IResourceManageService resourceManageService
            )
        {
            OrchardService = orchardServices;
            _resourceManageService = resourceManageService;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices OrchardService { get; set; }
        public Localizer T { get; set; }

        // GET: Classify
        public ActionResult Index()
        {
            return View();
        }

        [Themed]
        public ActionResult Add()
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceAdmin,T("需要更高的权限建立分类")))
                return new HttpUnauthorizedResult();
            return View();
        }

        [HttpPost,ActionName("Add")]
        [Themed]
        public ActionResult AddPost(string resType,string clsName,string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(clsName))
                ModelState.AddModelError("clsName", "分类名不能为空");
            var resTypeResult = _resourceManageService.GetResTypeByName(resType);
            if (resTypeResult == null)
                ModelState.AddModelError("resType", "找不到对应资源类型，无法添加分类");
            else if (resTypeResult.Classes.Select(i => i.ClsName).Contains(clsName))
                ModelState.AddModelError("clsName", "该分类已存在，无需创建");
            if (!ModelState.IsValid)
                return View();
            _resourceManageService.NewClassify(clsName, resTypeResult);

            return this.RedirectLocal(returnUrl, () => RedirectToAction("Index"));
        }

        [HttpPost]
        public ActionResult Remove(int id,string returnUrl)
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceAdmin, T("需要更高权限删除分类")))
                return new HttpUnauthorizedResult();

            var clsRecord = _resourceManageService.GetClsById(id);
            if (clsRecord == null)
                return new HttpNotFoundResult();

            _resourceManageService.DeleteClassify(id);

            return this.RedirectLocal(returnUrl, () => RedirectToAction("Index"));
        }

        /// <summary>
        /// 使用json返回选定的resType拥有的分类，列表呈现
        /// </summary>
        /// <param name="resType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCatagory(string resType)
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("不具查询分类的权限")))
                return new HttpUnauthorizedResult();
            var catagoryResult = _resourceManageService.GetCatagory(resType);
            if (catagoryResult == null) return Json(false);
            var jsonStr = JsonConvert.SerializeObject(
                new
                {
                    catagory = catagoryResult.Select(i => new
                    {
                        name = i.ClsName,
                        clsid = "cata-" + i.Id
                    }).ToArray()
                });
            return new JsonResult() { Data = jsonStr };
        }

        [HttpPost]
        public ActionResult GetResourceEditList(int clsId)
        {
            if(!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic,T("不具有查询资源的权限")))
                return new HttpUnauthorizedResult();
            var resResult = _resourceManageService.GetClssifiedResources(clsId);
            if (resResult == null)
                return Json(false);
            var clsResult = _resourceManageService.GetClsById(clsId);
            if (clsResult == null)
                return Json(false);
            var typeResourceResult = clsResult.ResourceType.Resources;
            var jsonObj = typeResourceResult.Select(i => new { Id = i.Id, Content = i.Content, Classified = resResult.Contains(i) }).ToArray();

            return new JsonResult() { Data = JsonConvert.SerializeObject(new { resources = jsonObj }) };
        }

        [Themed]
        public ActionResult Catagory()
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("不具有查询分类的权限")))
                return new HttpUnauthorizedResult();

            var viewModel = new CatagoryEditViewModel();

            var resTypeRecords = _resourceManageService.ResourceTypes();
            if (resTypeRecords.Count() < 1)
                return View(viewModel);

            foreach(var i in resTypeRecords)
            {
                var resTypeModel = new ResTypeModel { TypeId = i.Id, DisplayName = i.DisplayName };
                var catagoriesModel = i.Classes.Select(cata => new CatagoryModel { CataId = cata.Id, DisplayName = cata.ClsName });
                resTypeModel.Catagories = catagoriesModel;
                viewModel.ResTypes.Add(resTypeModel);
            }
            
            return View(viewModel);
        }

        [Themed]
        public ActionResult CatagoryEdit(int id)
        {

            return View();
        }

        [HttpPost,ActionName("CatagoryEdit")]
        public ActionResult CatagoryEditPost(IEnumerable<int> resIds)
        {
            List<string> a = new List<string>();
            foreach(var i in resIds)
            {
                a.Add(Convert.ToString(i));
            }
            return Content(string.Join(",", a));
        }
    }
}