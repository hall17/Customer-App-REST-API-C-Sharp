using CustomerAppDAL.Context;
using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerAppDAL.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CustomerAppContext _context;

        public AddressRepository(CustomerAppContext context)
        {
            _context = context;
        }
        public Address Create(Address address)
        {
            _context.Addresses.Add(address);
            return address;
        }

        public Address Delete(int Id)
        {
            var address = Get(Id);
            _context.Addresses.Remove(address);
            return address;
        }

        public Address Get(int Id)
        {
            return _context.Addresses.FirstOrDefault(o => o.Id == Id);
        }

        public List<Address> GetAll()
        {
            return _context.Addresses.ToList();
        }

        IEnumerable<Address> IAddressRepository.GetAllById(List<int> ids)
        {
            if (ids == null) return null;
            return  _context.Addresses.Where(a => ids.Contains(a.Id));
        }
    }
}
