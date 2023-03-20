using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Realta.Contract.Models.v1.Hotels
{
    public class HotelSwitchDto
    {
        [Required(ErrorMessage = "hotel id is required")]
        public int HotelId { get; set; }
        [Required(ErrorMessage = "Hotel status is required")]
        public bool HotelStatus { get; set; }
        [AllowNull]
        public string? HotelReasonStatus { get; set; }
    }
}
