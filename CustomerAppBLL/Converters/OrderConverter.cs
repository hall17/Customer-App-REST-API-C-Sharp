using CustomerAppBLL.BusinessObjects;
using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppBLL.Converters
{
    class OrderConverter
    {
        internal Order Convert(OrderBO order)
        {
            if (order == null) { return null; }

            return new Order()
            {
                Id = order.Id,
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                //Customer = new CustomerConverter().Convert(order.Customer),
                CustomerId = order.CustomerId
            };
        }
        internal OrderBO Convert(Order order)
        {
            if (order == null) { return null; }

            return new OrderBO()
            {
                Id = order.Id,
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                Customer = new CustomerConverter().Convert(order.Customer),
                CustomerId = order.CustomerId
            };
        }
    }
}
