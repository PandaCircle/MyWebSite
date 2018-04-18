using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Models
{
    public class OrderDetail
    {
        public string PersonName { get; set; }
        public string Department { get; set; }
        public DateTime OrderTime { get; set; }
        public List<ItemDetail> Items { get; set; }
        
    }
}