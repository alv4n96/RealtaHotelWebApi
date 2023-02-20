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
        private IHotelReviewsRepository? _hotelReviewsRepository;
        private IFacilitiesRepository? _facilitiesRepository;

        public RepositoryManager(AdoDbContext adoContext) => _adoContext = adoContext;

        public IHotelsRepository HotelsRepository => _hotelsRepository ??= new HotelsRepository(_adoContext);

        public IHotelReviewsRepository HotelReviewsRepository => _hotelReviewsRepository ??= new HotelReviewsRepository(_adoContext);

        public IFacilitiesRepository FacilitiesRepository => _facilitiesRepository ??= new FacilitiesRepository(_adoContext);
    }
}
