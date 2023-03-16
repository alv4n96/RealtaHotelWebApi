using Realta.Domain.RequestFeatures;
using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realta.Domain.RequestFeatures.HotelParameters;

namespace Realta.Domain.Repositories.v1
{
    public interface IFacilitiesRepository
    {
        IEnumerable<Facilities> FindAllFacilities();
        Task<IEnumerable<Facilities>> FindAllFacilitiesAsync(int hotelId);
        Task<Facilities> FindFacilitiesByIdAsync(int hotelId, int facilitiesId);
        Task<PagedList<Facilities>> GetFacilitiesPageList(FacilitiesParameters facilitiesParam, int hotelId);
        void Insert(Facilities hotelReviews);
        void Edit(Facilities hotelReviews);
        void Remove(Facilities hotelReviews);
    }
}
