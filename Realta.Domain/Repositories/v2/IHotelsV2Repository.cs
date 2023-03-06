using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories.v2
{
    public interface IHotelsV2Repository
    {
        IEnumerable<Hotels> FindAllHotels();

        Task<IEnumerable<Hotels>> FindAllHotelsAsync();

        Hotels FindHotelsById(int hotelId);

        //HotelsNestedFacilities GetHotelsFacilities(int hotelId);

        int GetSequenceId(string sql);

        void Insert(Hotels supplier);

        void Edit(Hotels supplier);

        void Remove(Hotels supplier);
    }
}
