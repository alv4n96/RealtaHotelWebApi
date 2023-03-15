using Realta.Contract.Models.v1.Hotels;

namespace Realta.Contract.Models.v1.Facilities;


public class HotelFaciAllDto
{
        public HotelsDto? Hotels { get; set; }
        public IEnumerable<FacilitiesDto> Facilities { get; set; }
}