using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IHotelsRepository
    {
        IEnumerable<Hotels> FindAllVendor();
        Task<IEnumerable<Hotels>> FindAllVendorAsync();
        Hotels FindVendorById(int id);
        int Insert(Hotels vendor);
        void Edit(Hotels vendor);
        void Remove(Hotels vendor);
    }
}
