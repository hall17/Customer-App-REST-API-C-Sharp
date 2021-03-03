using CustomerAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppDAL
{
    public interface IAddressRepository
    {
        // C
        Address Create(Address address);

        // R
        List<Address> GetAll();
        IEnumerable<Address> GetAllById(List<int> ids);
        Address Get(int id);

        // U
        // No update for repository, it will be the task of unit of work.

        // D
        Address Delete(int id);
    }
}
