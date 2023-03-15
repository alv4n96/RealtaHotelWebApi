using Realta.Contract.Models.v1.Hotels;

namespace Realta.Contract.Models.v1.Reviews;


public class HotelReviewsAllDto
{
        public HotelsDto? Hotels { get; set; }
        public IEnumerable<HotelReviewsDto> Reviews { get; set; }
}