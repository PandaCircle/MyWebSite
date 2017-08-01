using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.Models
{
    public class ClsResRecord
    {
        public virtual int Id { get; set; }
        public virtual ResourceRecord Resource { get; set; }
        public virtual ClassifyRecord Classify { get; set; }
    }
}