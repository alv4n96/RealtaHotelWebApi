using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IFacilitiesRepository
    {
        IEnumerable<Facilities> FindAllFacilities();
        Task<IEnumerable<Facilities>> FindAllFacilitiesAsync(int hotelId);
        Task<Facilities> FindFacilitiesByIdAsync(int hotelId, int facilitiesId);
        void Insert(Facilities hotelReviews);
        void Edit(Facilities hotelReviews);
        void Remove(Facilities hotelReviews);
    }
}
