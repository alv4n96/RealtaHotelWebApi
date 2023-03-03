using Realta.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Realta.Contract.Models
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
        public int? FaciMaxNumber { get; set; }
        [AllowNull]
        [MaxLength(15, ErrorMessage = "Max length for input is 15")]
        public string? FaciMeasureUnit { get; set; }
        [Required(ErrorMessage = "Facility room number is required")]
        [MaxLength(6, ErrorMessage = "Max length for input is 6")]
        public string? FaciRoomNumber { get; set; }
        [Required(ErrorMessage = "Facility start date is required")]
        public DateTime FaciStartdate { get; set; }
        [Required(ErrorMessage = "Facility end date is required")]
        public DateTime FaciEndate { get; set; }
        [Required(ErrorMessage = "Facility low price is required")]
        public Decimal FaciLowPrice { get; set; }
        [Required(ErrorMessage = "Faci high price is required")]
        public Decimal FaciHighPrice { get; set; }
        public Decimal FaciRatePrice { get; set; }
        [AllowNull]
        public Decimal? FaciDiscount { get; set; }
        [AllowNull]
        public Decimal? FaciTaxRate { get; set; }
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
