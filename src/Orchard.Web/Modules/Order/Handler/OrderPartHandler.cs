using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Handler
{
    public class OrderPartHandler:ContentHandler
    {
        public OrderPartHandler
            (
               IRepository<OrderRecord> orderRepository,
               IRepository<ItemDetailRecord> itemRepository
            )
        {
            Filters.Add(StorageFilter.For(orderRepository));

            OnInitializing<OrderPart>((ctx, part) =>
            {
                part.Items = new List<ItemDetail>();
            });


            OnLoading<OrderPart>((context, part) =>
            {
                part.ItemsField.Loader(() =>
                {
                    return itemRepository.Fetch(x => x.OrderRecord.Id == context.ContentItem.Id).Select(x => new ItemDetail { ItemName = x.ItemName, Quantity = x.Quantity, Remark = x.Remark }).ToList();
                });
            });
        }

        
    }
}