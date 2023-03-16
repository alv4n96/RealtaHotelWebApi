using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories.v1
{
    public interface IFacilityPriceHistoryRepository
    {
        Task<IEnumerable<FacilityPriceHistory>> FindAllFacilityPriceHistoryAsync(int hotelId);
        Task<IEnumerable<FacilityPriceHistory>> FindAllFacilityPriceHistoryByFacilityAsync(int hotelId, int faciId);
        Task<FacilityPriceHistory> FindAllFacilityPriceHistoryByIdAsync(int faciId, int faphId);
        Task<PagedList<FacilityPriceHistory>> GetFacilityPriceHistoryPageList(HistoryParameters historyParam, int hotelId, int faciId);
    }
}
