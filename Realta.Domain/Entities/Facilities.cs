using Realta.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Facilities")]
    public class Facilities
    {
        [Key]
        public int faci_id { get; set; }
        public string? faci_name { get; set; }
        [AllowNull]
        public string? faci_description { get; set; }
        [AllowNull]
        public int faci_max_number { get; set; }
        [AllowNull]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public faci_measure_unit? faci_measure_unit { get; set; }
        public string? faci_room_number { get; set; }
        public DateTime faci_startdate { get; set; }
        public DateTime faci_endate { get; set; }
        public decimal   faci_low_price { get; set; }
        public decimal faci_high_price { get; set; }
        public decimal   faci_rate_price { get; set; }
        [AllowNull]
        public float faci_discount { get; set; }
        [AllowNull]
        public float  faci_tax_rate { get; set; }
        [AllowNull]
        public DateTime faci_modified_date { get; set; }
        public int faci_cagro_id { get; set; }
        public int faci_hotel_id { get; set; }
        public int faci_user_id { get; set; }
    }
}
