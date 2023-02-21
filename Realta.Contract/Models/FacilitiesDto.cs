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
        [Required(ErrorMessage = "facility id is required")]
        public int faci_id { get; set; }
        [Required(ErrorMessage = "facility name is required")]
        public string? faci_name { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? faci_description { get; set; }
        [AllowNull]
        public int? faci_max_number { get; set; }
        [AllowNull]
        [MaxLength(15, ErrorMessage = "Max length for input is 15")]
        public string? faci_measure_unit { get; set; }
        [Required(ErrorMessage = "facility room number is required")]
        [MaxLength(6, ErrorMessage = "Max length for input is 6")]
        public string? faci_room_number { get; set; }
        [Required(ErrorMessage = "facility start date is required")]
        public DateTime faci_startdate { get; set; }
        [Required(ErrorMessage = "facility end date is required")]
        public DateTime faci_endate { get; set; }
        [Required(ErrorMessage = "facility low price is required")]
        public Decimal faci_low_price { get; set; }
        [Required(ErrorMessage = "faci high price is required")]
        public Decimal faci_high_price { get; set; }
        public Decimal faci_rate_price { get; set; }
        [AllowNull]
        public Decimal? faci_discount { get; set; }
        [AllowNull]
        public Decimal? faci_tax_rate { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime faci_modified_date { get; set; }
        [Required(ErrorMessage = "Cagro id is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int faci_cagro_id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int faci_hotel_id { get; set; }
        [Required(ErrorMessage = "user id is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int faci_user_id { get; set; }
    }
}
