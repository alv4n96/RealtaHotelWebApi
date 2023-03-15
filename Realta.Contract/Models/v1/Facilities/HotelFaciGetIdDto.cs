using Realta.Contract.Models.v1.Hotels;

namespace Realta.Contract.Models.v1.Facilities;

public class HotelFaciByIdDto
{
    public HotelsDto? Hotels { get; set; }
    public FacilitiesDto? Facilities { get; set; }
}