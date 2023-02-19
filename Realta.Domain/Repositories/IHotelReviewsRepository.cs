using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IHotelReviewsRepository
    {
        IEnumerable<Hotel_Reviews> FindAllHotelReviews();
        Task<IEnumerable<Hotel_Reviews>> FindAllHotelReviewsAsync(int hotelId);
        Hotel_Reviews FindHotelReviewsById(int hoReId);
        void Insert(Hotel_Reviews hotels);
        void Edit(Hotel_Reviews hotels);
        void EditStatus(Hotel_Reviews hotels);
        void Remove(Hotel_Reviews hotels);
    }
}
