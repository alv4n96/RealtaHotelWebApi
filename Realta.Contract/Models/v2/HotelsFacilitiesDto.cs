using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Hotels;
using System;
using System.Threading.Tasks;

namespace Realta.Contract.Models.v2
{
    public class HotelsFacilitiesDto
    {
        public HotelsDto? HotelsDto { get; set; }
        public virtual ICollection<FacilitiesDto>? FacilitiesDto { get; set; }
    }
}
