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
    public class AddressService : IAddressService
    {
        private readonly DALFacade facade;
        AddressConverter conv;

            public AddressService(DALFacade facade)
        {
            this.facade = facade;
            conv = new AddressConverter();
        }
        public AddressBO Create(AddressBO address)
        {
            using (var uow = facade.UnitOfWork)
            {
                var newAddress = uow.AddressRepository.Create(conv.Convert(address));
                uow.Complete();
                return conv.Convert(newAddress);
            }
        }

        public AddressBO Delete(int id)
        {
            using (var uow = facade.UnitOfWork)
            {
                var Address = uow.AddressRepository.Delete(id);
                uow.Complete();
                return conv.Convert(Address);
            }
        }

        public AddressBO Get(int id)
        {
            using (var uow = facade.UnitOfWork)
            {
                return conv.Convert(uow.AddressRepository.Get(id));
            }
        }

        public List<AddressBO> GetAll()
        {
            using (var uow = facade.UnitOfWork)
            {
                // Customer -> CustomerBO
                //  return uow.CustomerRepository.GetAll();
                return uow.AddressRepository.GetAll().Select(conv.Convert).ToList();
            }
        }

        public AddressBO Update(AddressBO address)
        {
            using (var uow = facade.UnitOfWork)
            {
                Address addressFromDb = uow.AddressRepository.Get(address.Id);
                if (addressFromDb == null)
                {
                    throw new InvalidOperationException("Address not found");
                }
                addressFromDb.City = address.City;
                addressFromDb.Number = address.Number;
                addressFromDb.Street = address.Street;
                uow.Complete();
                return conv.Convert(addressFromDb);
            }
        }
    }
}
