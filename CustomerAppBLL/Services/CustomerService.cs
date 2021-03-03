using CustomerAppBLL.BusinessObjects;
using CustomerAppBLL.Converters;
using CustomerAppDAL;
using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerAppBLL.Services
{
    class CustomerService : ICustomerService
    {
        DALFacade facade;
        CustomerConverter conv = new CustomerConverter();
        AddressConverter aConv = new AddressConverter();
        public CustomerService(DALFacade facade)
        {
            this.facade = facade;
        }
        public CustomerBO Create(CustomerBO cust)
        {
            using (var uow = facade.UnitOfWork)
            {
                var newCust = uow.CustomerRepository.Create(conv.Convert(cust));
                uow.Complete();
                return conv.Convert(newCust);
            }
        }

        public CustomerBO Delete(int id)
        {
            using (var uow = facade.UnitOfWork)
            {
                var deletedCust = uow.CustomerRepository.Delete(id);
                uow.Complete();
                return conv.Convert(deletedCust);
            }
        }

        public CustomerBO Get(int id)
        {
            using (var uow = facade.UnitOfWork)
            {
                // 1. Get and convert the customer
                var cust = conv.Convert(uow.CustomerRepository.Get(id));

                // 2. Get All related Addresses from AddressRepository using addressIds
                // 3. Convert and Add the addresses to the CustomerBO
                //cust.Addresses = cust.AddressIds?
                //    .Select(id => aConv.Convert(uow.AddressRepository.Get(id)))
                //    .ToList();
                cust.Addresses = uow.AddressRepository.GetAllById(cust.AddressIds)
                    .Select(a => aConv.Convert(a))
                    .ToList();

                // 4. Return the Customer
                return cust;
            }
        }

        public List<CustomerBO> GetAll()
        {
            using (var uow = facade.UnitOfWork)
            {
                // Customer -> CustomerBO
                //  return uow.CustomerRepository.GetAll();
                return uow.CustomerRepository.GetAll().Select(conv.Convert).ToList();
            }
        }

        public CustomerBO Update(CustomerBO cust)
        {
            using (var uow = facade.UnitOfWork)
            {
                Customer customerFromDb = uow.CustomerRepository.Get(cust.Id);
                if (customerFromDb == null)
                {
                    throw new InvalidOperationException("Customer not found");
                }
                var customerUpdated = conv.Convert(cust);
                customerFromDb.FirstName = customerUpdated.FirstName;
                customerFromDb.LastName = customerUpdated.LastName;
                customerFromDb.Addresses = customerUpdated.Addresses;


                uow.Complete();
                return conv.Convert(customerFromDb);
            }

        }

    }
}
