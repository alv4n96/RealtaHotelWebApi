using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Realta.Contract.Models.v1.Hotels
{
    public class HotelSwitchDto
    {
        [Required(ErrorMessage = "hotel id is required")]
        public int HotelId { get; set; }
        [Required(ErrorMessage = "Hotel status is required")]
        public string? HotelStatus { get; set; }
        [AllowNull]
        public string? HotelReasonStatus { get; set; }
    }
}
