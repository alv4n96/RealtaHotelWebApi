using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IHotelsRepository
    {
        IEnumerable<Hotels> FindAllHotels();
        Task<IEnumerable<Hotels>> FindAllHotelsAsync();
        Hotels FindHotelsById(int hotelId);
        void Insert(Hotels hotels);
        void Edit(Hotels hotels);
        void Remove(Hotels hotels);
    }
}
