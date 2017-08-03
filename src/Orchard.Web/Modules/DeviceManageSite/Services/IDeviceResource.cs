using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.Services
{
    public interface IDeviceResource:IDependency
    {
        string ResourceType { get; }
        ContentPart Part { get; }
    }
}