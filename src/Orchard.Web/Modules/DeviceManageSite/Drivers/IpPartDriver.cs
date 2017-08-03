using DeviceManageSite.Models;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Department.Helper;
using DeviceManageSite.Services;
using Orchard;
using Orchard.UI.Notify;
using Orchard.Localization;

namespace DeviceManageSite.Drivers
{
    public class IpPartDriver:ContentPartDriver<IpPart>
    {
        private readonly IResourceManageService _resourceManageService;
        public IpPartDriver(
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

        protected override DriverResult Editor(IpPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_IpPart_Edit",
                ()=>shapeHelper.EditorTemplate(TemplateName: "Parts/IpPart.Edit", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(IpPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            List<string> importList;

            if (RegIp.TryGetIp(part.Start, part.End, out importList))
            {
                var typeRecord = _resourceManageService.GetResTypeByName(part.ResourceType);
                if (typeRecord == null)
                {
                    updater.AddModelError("resType", T("当前添加的资源类型不存在"));
                    return Editor(part, shapeHelper);
                }
                foreach (var i in importList)
                {
                    _resourceManageService.NewResource(i, typeRecord);
                }
                OrchardService.Notifier.Information(T("添加成功"));
            }
            else updater.AddModelError("Start", T("ip地址输入有误，请检查"));
            return Editor(part,shapeHelper);
        }
    }
}