using Realta.Domain.Base;
using Realta.Domain.Repositories;
using Realta.Persistence.Repositories;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private AdoDbContext _adoContext;
        private IHotelsRepository? _hotelsRepository;

        public RepositoryManager(AdoDbContext adoContext) => _adoContext = adoContext;

        public IHotelsRepository HotelsRepository
        {
            get
            {
                if (_hotelsRepository == null)
                {
                    _hotelsRepository = new HotelsRepository(_adoContext);
                }
                return _hotelsRepository;
            }
        }

    }
}
