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
    public interface IHotelsRepository
    {
        IEnumerable<Hotels> FindAllHotels();
        Task<IEnumerable<Hotels>> FindAllHotelsAsync();
        IEnumerable<Hotels> FindHotelsByName(string name);
        Task<IEnumerable<Hotels>> GetHotelPaging(HotelsParameters hotelParam);
        Task<PagedList<Hotels>> GetHotelPageList(HotelsParameters hotelParam);
        Task<Hotels> FindHotelsByIdAsync(int hotelId);
        void Insert(Hotels hotels);
        void Edit(Hotels hotels);
        void EditStatus(Hotels hotels);
        void Remove(Hotels hotels);
    }
}
