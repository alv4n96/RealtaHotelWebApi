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
        Task<Hotel_Reviews> FindHotelReviewsByIdAsync(int hotelId, int hotelReviewsId);
        void Insert(Hotel_Reviews hotelReviews);
        void Edit(Hotel_Reviews hotelReviews);
        void Remove(Hotel_Reviews hotelReviews);
    }
}
