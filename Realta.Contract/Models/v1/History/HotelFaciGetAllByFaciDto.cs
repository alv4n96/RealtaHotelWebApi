using Realta.Contract.Models.v1.Hotels;

namespace Realta.Contract.Models.v1.History;


public class HotelFaciHistoryGetByFaciDto
{
        public HotelsDto? Hotels { get; set; }
        public FacilitiesDto? Facility { get; set; }
        public IEnumerable<FacilityPriceHistoryDto> History { get; set; }
}
