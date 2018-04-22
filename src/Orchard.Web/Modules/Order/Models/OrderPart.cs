using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Models
{
    public class OrderPart:ContentPart<OrderRecord>
    {
        private readonly LazyField<IList<ItemDetail>> _items = new LazyField<IList<ItemDetail>>();

        public LazyField<IList<ItemDetail>> ItemsField { get { return _items; } }

        public string PersonName
        {
            get { return Record.Proposer; }
            set { Record.Proposer = value; }
        }
        public string Department
        {
            get { return Record.Department; }
            set { Record.Department = value; }
        }
        public DateTime OrderTime
        {
            get { return Record.CreateTime; }
            set { Record.CreateTime = value; }
        }
        public IList<ItemDetail> Items
        {
            get { return _items.Value; }
            set { _items.Value = value; }
        }
        
    }
}