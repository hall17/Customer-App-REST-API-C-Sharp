using CustomerAppDAL.Context;
using CustomerAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerAppDAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CustomerAppContext _context;

        public OrderRepository(CustomerAppContext context)
        {
            _context = context;
        }
        public Order Create(Order order)
        {
            //if(order.Customer != null)
            //{
            //    _context.Entry(order.Customer).State = EntityState.Unchanged;
            //}
            _context.Orders.Add(order);
            return order;
        }

        public Order Delete(int Id)
        {
            var order = Get(Id);
            _context.Orders.Remove(order);
            return order;
        }

        public Order Get(int Id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == Id);
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
    }
}
