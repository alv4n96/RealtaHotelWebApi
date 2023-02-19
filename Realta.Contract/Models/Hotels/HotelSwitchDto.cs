using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Realta.Contract.Models.Hotels
{
    public class HotelSwitchDto
    {
        [Required(ErrorMessage = "hotel id is required")]
        public int hotel_id { get; set; }
        [Required(ErrorMessage = "hotel status is required")]
        public string? hotel_status { get; set; }
        [AllowNull]
        public string? hotel_reason_status { get; set; }
    }
}
