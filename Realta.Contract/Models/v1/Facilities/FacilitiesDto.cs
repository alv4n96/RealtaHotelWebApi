using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Realta.Contract.Models.v1
{
    public class FacilitiesDto
    {
        [Required(ErrorMessage = "Facility id is required")]
        public int FaciId { get; set; }
        [Required(ErrorMessage = "Facility name is required")]
        public string? FaciName { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FaciDescription { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? FaciMaxNumber { get; set; }
        [AllowNull]
        [MaxLength(15, ErrorMessage = "Max length for input is 15")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FaciMeasureUnit { get; set; }
        [Required(ErrorMessage = "Facility room number is required")]
        [MaxLength(6, ErrorMessage = "Max length for input is 6")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FaciRoomNumber { get; set; }
        [Required(ErrorMessage = "Facility start date is required")]
        public DateTime FaciStartdate { get; set; }
        [Required(ErrorMessage = "Facility end date is required")]
        public DateTime FaciEndDate { get; set; }
        [Required(ErrorMessage = "Facility low price is required")]
        public decimal FaciLowPrice { get; set; }
        [Required(ErrorMessage = "Faci high price is required")]
        public decimal FaciHighPrice { get; set; }
        public decimal FaciRatePrice { get; set; }
        
        public Int16 FaciExposePrice { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? FaciDiscount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public decimal? FaciTaxRate { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime FaciModifiedDate { get; set; }
        [Required(ErrorMessage = "Cagro id is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FaciCagroId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FaciHotelId { get; set; }
        [Required(ErrorMessage = "user id is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FaciUserId { get; set; }
    }
}
