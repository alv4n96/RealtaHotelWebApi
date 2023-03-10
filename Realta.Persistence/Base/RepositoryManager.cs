using Realta.Domain.Base;
using Realta.Domain.Repositories.v1;
using Realta.Persistence.Repositories;
using Realta.Persistence.Repositories.v1;
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
        private IFacilityPhotosRepository? _facilityPhotosRepository;
        private IFacilityPriceHistoryRepository? _facilityPriceHistoryRepository;

        public RepositoryManager(AdoDbContext adoContext) => _adoContext = adoContext;

        public IHotelsRepository HotelsRepository => _hotelsRepository ??= new HotelsRepository(_adoContext);

        public IHotelReviewsRepository HotelReviewsRepository => _hotelReviewsRepository ??= new HotelReviewsRepository(_adoContext);

        public IFacilitiesRepository FacilitiesRepository => _facilitiesRepository ??= new FacilitiesRepository(_adoContext);

        public IFacilityPhotosRepository FacilityPhotosRepository => _facilityPhotosRepository ??= new FacilityPhotosRepository(_adoContext);

        public IFacilityPriceHistoryRepository FacilityPriceHistoryRepository => _facilityPriceHistoryRepository ??= new FacilityPriceHistoryRepository(_adoContext);
    }
}
