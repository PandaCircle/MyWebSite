using DeviceManageSite.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Mvc.Extensions;
using Orchard.Themes;
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

        public ResourceController(
            IOrchardServices orchardService,
            IEnumerable<IDeviceResource> resourceParts
            )
        {
            OrchardService = orchardService;
            _contentManager = OrchardService.ContentManager;
            _resourceParts = resourceParts;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices OrchardService { get; set; }
        public Localizer T { get; set; }

        // GET: Resource
        [Themed]
        public ActionResult Index()
        {
            return View();
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