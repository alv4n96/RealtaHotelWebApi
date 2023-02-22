using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class FacilityPriceHistoryDto
    {
        public int faph_id { get; set; }
        public DateTime faph_startdate { get; set; }
        public DateTime faph_enddate { get; set; }
        public Decimal faph_low_price { get; set; }
        public Decimal faph_high_price { get; set; }
        public Decimal faph_rate_price { get; set; }
        public Decimal faph_discount { get; set; }
        public Decimal faph_tax_rate { get; set; }
        public DateTime faph_modified_date { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int faph_faci_id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int faph_user_id { get; set; }
    }
}
