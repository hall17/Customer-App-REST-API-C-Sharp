using CustomerAppDAL.Entities;
using System.Collections.Generic;

namespace CustomerAppDAL
{
    public interface ICustomerRepository
    {
        // C
        Customer Create(Customer cust);

        // R
        List<Customer> GetAll();
        Customer Get(int id);
         
        // U
        // No update for repository, it will be the task of unit of work.

        // D
        Customer Delete(int id);
    }
}
