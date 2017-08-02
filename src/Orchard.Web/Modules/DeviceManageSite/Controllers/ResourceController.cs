using Orchard;
using Orchard.Localization;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceManageSite.Controllers
{
    public class ResourceController : Controller
    {

        public ResourceController(
            IOrchardServices orchardService
            )
        {
            OrchardService = orchardService;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices OrchardService { get; set; }
        public Localizer T { get; set; }

        // GET: Resource
        public ActionResult Index()
        {
            return View();
        }

        [Themed]
        public ActionResult Add()
        {
            if (!OrchardService.Authorizer.Authorize(Permissions.ResourceBasic, T("需要更高权限添加资源")))
                return new HttpUnauthorizedResult();
            return View();
        }

    }
}