using Orchard.Data.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.Models
{
    public class ResourcePart
    {
        
    }

    public class ResourceRecord
    {
        public virtual int Id { get; set; }
        public virtual string Content { get; set; }
        /// <summary>
        /// 0 代表没关联，关联unitId
        /// </summary>
        public virtual int AttachUnit { get; set; }

        public virtual ResourceTypeRecord ResourceType { get; set; }
        [CascadeAllDeleteOrphan]
        public virtual IList<ClsResRecord> Classes { get; set; }
    }

    public class ResourceTypeRecord
    {
        public virtual int Id { get; set; }
        public virtual string DisplayName { get; set; }
        [CascadeAllDeleteOrphan]
        public virtual IList<ResourceRecord> Resources { get; set; }
        [CascadeAllDeleteOrphan]
        public virtual IList<ClassifyRecord> Classes { get; set; }
    }

}