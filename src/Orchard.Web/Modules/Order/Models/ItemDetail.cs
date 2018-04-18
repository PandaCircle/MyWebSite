using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Models
{
    public class ItemDetail
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string Remark { get; set; }
    }
}