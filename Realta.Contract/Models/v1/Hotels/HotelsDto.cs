using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Realta.Contract.Models.v1.Hotels
{
    public class HotelsDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Required(ErrorMessage = "hotel id is required")]
        public int HotelId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Required(ErrorMessage = "Hotel name is required")]
        public string? HotelName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HotelDescription { get; set; }
        [Required(ErrorMessage = "Hotel status is required")]
        public bool HotelStatus { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HotelReasonStatus { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [AllowNull]
        public decimal HotelRatingStar { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Phone]
        [Required(ErrorMessage = "Hotel phone number is required")]
        public string? HotelPhonenumber { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [AllowNull]
        public DateTime HotelModifiedDate { get; set; }
        [Required(ErrorMessage = "Hotel address is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int HotelAddrId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HotelAddrDescription { get; set; }
    }
}