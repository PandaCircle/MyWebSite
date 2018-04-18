using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Models
{
    public class ItemDetailRecord
    {
        public virtual int Id { get; set; }
        public virtual string ItemName { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string Remark { get; set; }
        public virtual OrderRecord OrderRecord { get; set; }
    }
}