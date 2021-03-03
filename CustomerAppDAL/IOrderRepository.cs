using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppDAL
{
    public interface IOrderRepository
    {
        // C
        Order Create(Order order);

        // R
        List<Order> GetAll();
        Order Get(int id);

        // U
        // No update for repository, it will be the task of unit of work.

        // D
        Order Delete(int id);
    }
}
