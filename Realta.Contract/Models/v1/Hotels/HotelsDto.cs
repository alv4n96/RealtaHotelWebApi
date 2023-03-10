using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Realta.Contract.Models.v1.Hotels
{
    public class HotelsDto
    {
        [Required(ErrorMessage = "hotel id is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Hotel name is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        
        public string? HotelName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HotelDescription { get; set; }
        [Required(ErrorMessage = "Hotel status is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HotelStatus { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HotelReasonStatus { get; set; }
        [AllowNull]
        public decimal HotelRatingStar { get; set; }

        [Phone]
        [Required(ErrorMessage = "Hotel phone number is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HotelPhonenumber { get; set; }

        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime HotelModifiedDate { get; set; }
        [Required(ErrorMessage = "Hotel address is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? HotelAddrId { get; set; }
    }
}