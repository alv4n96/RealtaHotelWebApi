using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class HotelsRepository : RepositoryBase<Hotels>, IHotelsRepository
    {
        public HotelsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }


        public Task<IEnumerable<Hotels>> FindAllVendorAsync()
        {
            throw new NotImplementedException();
        }
        public void Edit(Hotels vendor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hotels> FindAllVendor()
        {
            throw new NotImplementedException();
        }

        public Hotels FindVendorById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Hotels vendor)
        {
            throw new NotImplementedException();
        }

        public void Remove(Hotels vendor)
        {
            throw new NotImplementedException();
        }
    }
}
