using Orchard.Data.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.Models
{
    public class ClassifyRecord
    {
        public virtual int Id { get; set; }
        public virtual string ClsName { get; set; }
        public virtual ResourceTypeRecord ResourceType { get; set; }
        [CascadeAllDeleteOrphan]
        public virtual IList<ClsResRecord> ClassifyResourceRecords { get; set; }
    }
}