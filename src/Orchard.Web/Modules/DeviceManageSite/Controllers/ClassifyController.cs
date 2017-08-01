using DeviceManageSite.Services;
using Orchard;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Mvc.Extensions;

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

        public ActionResult Add()
        {
            if (OrchardService.Authorizer.Authorize(Permissions.ResourceAdmin,T("需要更高的权限建立分类")))
                return new HttpUnauthorizedResult();
            return View();
        }

        [HttpPost,ActionName("Add")]
        public ActionResult AddPost(string resType,string clsName,string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(clsName))
                ModelState.AddModelError("clsName", "分类名不能为空");
            var resTypeResult = _resourceManageService.GetResTypeByName(resType);
            if (resType == null)
                ModelState.AddModelError("resType", "找不到对应资源类型，无法添加分类");
            if (resTypeResult.Classes.Select(i => i.ClsName).Contains(clsName))
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

        [HttpPost]
        public ActionResult ResourceClssify(int[] ids,string clsName,string returnUrl)
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("不具有更改分类的权限")))
                return new HttpUnauthorizedResult();
            _resourceManageService.RemoveClassifyResource(ids, clsName);

            return this.RedirectLocal(returnUrl, () => RedirectToAction("Index"));
        }
    }
}