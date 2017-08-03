using DeviceManageSite.Services;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.Models
{
    public class IpPart : ContentPart, IDeviceResource
    {
        public string ResourceType
        {
            get
            {
                return "Ip地址";
            }
        }

        public string Start { get; set; }
        public string End { get; set; }

        public ContentPart Part
        {
            get
            {
                return this;
            }
        }
    }
}