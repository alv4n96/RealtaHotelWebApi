using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories.v1
{
    public interface IHotelReviewsRepository
    {
        IEnumerable<HotelReviews> FindAllHotelReviews();
        Task<IEnumerable<HotelReviews>> FindAllHotelReviewsAsync(int hotelId);
        Task<HotelReviews> FindHotelReviewsByIdAsync(int hotelId, int hotelReviewsId);
        void Insert(HotelReviews hotelReviews);
        void Edit(HotelReviews hotelReviews);
        void Remove(HotelReviews hotelReviews);
    }
}
