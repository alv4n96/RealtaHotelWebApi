using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.Interface;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        public VendorRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vendor> FindAllVendor()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vendor>> FindAllVendorAsync()
        {
            throw new NotImplementedException();
        }

        public Vendor FindVendorById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public void Remove(Vendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}
