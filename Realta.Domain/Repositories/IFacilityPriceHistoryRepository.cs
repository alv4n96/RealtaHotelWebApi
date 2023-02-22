using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IFacilityPriceHistoryRepository
    {
        Task<IEnumerable<Facility_Price_History>> FindAllFacilityPriceHistoryAsync(int hotelId);
        Task<IEnumerable<Facility_Price_History>> FindAllFacilityPriceHistoryByFacilityAsync(int hotelId, int faciId);
        Task<Facility_Price_History> FindAllFacilityPriceHistoryByIdAsync(int faciId, int faphId);
    }
}
