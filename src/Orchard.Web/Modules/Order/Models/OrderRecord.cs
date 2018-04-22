using Orchard.ContentManagement.Records;
using Orchard.Data.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Models
{
    public class OrderRecord:ContentPartRecord
    {
        public virtual DateTime CreateTime { get; set; }
        public virtual string Department { get; set; }
        public virtual string Proposer { get; set; }
        [CascadeAllDeleteOrphan]
        public virtual IList<ItemDetailRecord> ItemRecords { get; set; }

    }
}