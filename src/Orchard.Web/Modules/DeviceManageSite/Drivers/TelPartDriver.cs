using DeviceManageSite.Models;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using DeviceManageSite.Services;
using Orchard;
using Orchard.Localization;
using Orchard.UI.Notify;

namespace DeviceManageSite.Drivers
{
    public class TelPartDriver:ContentPartDriver<TelPart>
    {
        private readonly IResourceManageService _resourceManageService;
        public TelPartDriver(
            IResourceManageService resourceManageService,
            IOrchardServices orchardService
            )
        {
            _resourceManageService = resourceManageService;
            OrchardService = orchardService;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices OrchardService { get; set; }
        public Localizer T { get; set; }

        protected override DriverResult Editor(TelPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_TelPart_Edit",
                () => shapeHelper.EditorTemplate(TemplateName:"Parts/TelPart.Edit",Model:part,Prefix:Prefix)
                );
        }

        protected override DriverResult Editor(TelPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            var typeRecord = _resourceManageService.GetResTypeByName(part.ResourceType);
            if(typeRecord == null)
            {
                updater.AddModelError("resType", T("当前添加的资源类型不存在"));
                return Editor(part, shapeHelper);
            }

            _resourceManageService.NewResource(part.Tel, typeRecord);
            OrchardService.Notifier.Information(T("添加成功"));

            return Editor(part, shapeHelper);
        }
    }
}