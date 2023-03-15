using Realta.Contract.Models.v1.Hotels;

namespace Realta.Contract.Models.v1.Reviews;


public class HotelReviewsByIdDto
{
        public HotelsDto? Hotels { get; set; }
        public HotelReviewsDto? Reviews { get; set; }
}