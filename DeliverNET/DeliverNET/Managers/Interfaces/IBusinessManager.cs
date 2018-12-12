using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Data;

namespace DeliverNET.Managers.Interfaces
{
    public interface IBusinessManager
    {
        // Create
        bool Create(Business business);

        // Read
        Business Get(int id);
        Business Get(string title);
        List<Business> GetAll();

        // Delete
        bool DeleteAll();
    }
}
