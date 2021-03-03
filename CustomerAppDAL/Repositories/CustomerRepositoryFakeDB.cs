using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerAppDAL.Repositories
{
    class CustomerRepositoryFakeDB : ICustomerRepository
    {
        private static int id = 1;
        private static List<Customer> Customers = new List<Customer>();

        public Customer Create(Customer cust)
        {
            Customer newCust;
            Customers.Add(newCust = new Customer()
            {
                Id = id++,
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                Addresses = cust.Addresses
            });
            return newCust;
        }

        public Customer Delete(int id)
        {
            Customer cust = Customers.FirstOrDefault(c => c.Id == id);
            Customers.Remove(cust);
            return cust;
        }

        public Customer Get(int id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetAll()
        {
            return new List<Customer>(Customers);
        }
    }
}
