using CustomerAppDAL.Context;
using CustomerAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAppDAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        CustomerAppContext _context;

        public CustomerRepository(CustomerAppContext context)
        {
            _context = context;
        }
        public Customer Create(Customer cust)
        {          
            _context.Customers.Add(cust);
            return cust;
        }

        public Customer Delete(int id)
        {
            Customer cust = Get(id);
            _context.Customers.Remove(cust);
            return cust;
        }

        public Customer Get(int id)
        {
           return _context.Customers
                .Include(cus => cus.Addresses)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetAll()
        {
            return _context.Customers
                .Include(cus => cus.Addresses)
                //.ThenInclude(ca => ca.Address)    // to get full address
                .ToList();
        }
    }
}
