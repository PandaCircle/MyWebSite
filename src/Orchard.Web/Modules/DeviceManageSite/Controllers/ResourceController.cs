using DeviceManageSite.Services;
using DeviceManageSite.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Mvc.Extensions;
using Orchard.Themes;
using Orchard.UI.Admin;
using Orchard.UI.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceManageSite.Controllers
{
    public class ResourceController : Controller,IUpdateModel
    {
        private readonly IContentManager _contentManager;
        private readonly IEnumerable<IDeviceResource> _resourceParts;
        private readonly IResourceManageService _resourceManageService;

        public ResourceController(
            IOrchardServices orchardService,
            IEnumerable<IDeviceResource> resourceParts,
            IResourceManageService resourceManageService,
            IShapeFactory shapeFactory
            )
        {
            OrchardService = orchardService;
            _contentManager = OrchardService.ContentManager;
            _resourceParts = resourceParts;
            _resourceManageService = resourceManageService;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        public IOrchardServices OrchardService { get; set; }
        public dynamic Shape { get; set; }
        public Localizer T { get; set; }

        // GET: Resource
        [Themed]
        public ActionResult Index()
        {
            var typeResult = _resourceManageService.ResourceTypes();
            List<ResTypeViewModel> typeViewList = new List<ResTypeViewModel>();
            foreach(var i in typeResult)
            {
                ResTypeViewModel model = new ResTypeViewModel
                {
                    resTypeId = i.Id,
                    ResTypeName = i.DisplayName,
                    Remains = _resourceManageService.Remians(i.DisplayName)
                    //Remains = 1
                };
                typeViewList.Add(model);
            }
            var listShape = Shape.List();
            var viewModel = Shape.viewModel()
                .ContentItems(listShape)
                .ResTypeList(typeViewList);
            return View(viewModel);
        }

        [HttpPost,ActionName("List")]
        public ActionResult ListPost(string returnUrl)
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("需要更高权限删除资源")))
                return new HttpUnauthorizedResult();
            var viewModel = new ResIndexViewModel { DeviceResources = new List<ResViewModel>() };
            UpdateModel(viewModel);

            var checkedList = viewModel.DeviceResources.Where(i => i.IsChecked);
            foreach(var i in checkedList)
            {
                _resourceManageService.DeleteResource(i.ResId);
            }
            OrchardService.Notifier.Information(T("已删除"));
            return this.RedirectLocal(returnUrl, () => RedirectToAction("Index"));
        }

        [Themed]
        public ActionResult Add(string resType)
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("需要更高权限添加资源")))
                return new HttpUnauthorizedResult();
            var creator = _contentManager.New("ResourceCreator");
            var matchPart = _resourceParts.Where(i => i.ResourceType == resType).SingleOrDefault();
            if (matchPart == null)
                return new HttpNotFoundResult();
            creator.Weld(matchPart.Part);

            var creatorShape = _contentManager.BuildEditor(creator);
            //携带当前资源类型
            creatorShape.resType = resType;
            return View(creatorShape);
        }

        [HttpPost,ActionName("Add")]
        [Themed]
        public ActionResult AddPost(string resType,string returnUrl)
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("需要更高权限添加资源")))
                return new HttpUnauthorizedResult();
            var creator = _contentManager.New("ResourceCreator");
            var matchPart = _resourceParts.Where(i => i.ResourceType == resType).SingleOrDefault();
            if (matchPart == null)
                return new HttpNotFoundResult();
            creator.Weld(matchPart.Part);

            //让driver自己处理输入数据
            var creatorShape = _contentManager.UpdateEditor(creator,this);
            if (!ModelState.IsValid)
            {
                OrchardService.TransactionManager.Cancel();
                return View(creatorShape);
            }
            return this.RedirectLocal(returnUrl, () => RedirectToAction("Index"));
            
        }

        [Admin]
        public ActionResult List(int typeId =0)
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("需要更高权限查看资源")))
                return new HttpUnauthorizedResult();
            var typeResult = _resourceManageService.GetResTypeById(typeId);
            if (typeResult == null)
                return new HttpNotFoundResult();
            var list= _resourceManageService.GetResourcesByType(typeId);
            List<ResViewModel> resViewList = new List<ResViewModel>();
            foreach(var i in list)
            {
                ResViewModel model = new ResViewModel
                {
                    DisplayContent = i.Content,
                    Catagories = String.Join(",", i.Classes.Select(cls => cls.Classify.ClsName)),
                    ResId = i.Id,
                    Using = i.AttachUnit,
                    IsChecked = false
                };
                resViewList.Add(model);
            }
            ResIndexViewModel viewModel = new ResIndexViewModel { DeviceResources = resViewList ,ResType = typeResult.DisplayName};
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id,string returnUrl)
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("需要更高权限删除资源")))
                return new HttpUnauthorizedResult();
            _resourceManageService.DeleteResource(id);
            OrchardService.Notifier.Information(T("资源已删除"));
            return this.RedirectLocal(returnUrl, () => RedirectToAction("Index"));
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return this.TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}