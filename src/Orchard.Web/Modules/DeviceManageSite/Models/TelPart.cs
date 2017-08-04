using DeviceManageSite.Services;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.Models
{
    public class TelPart : ContentPart, IDeviceResource
    {
        public ContentPart Part
        {
            get
            {
                return this;
            }
        }

        public string ResourceType
        {
            get
            {
                return "电话";
            }
        }

        public string Tel { get; set; }
    }
}