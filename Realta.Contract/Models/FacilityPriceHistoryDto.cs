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
        public int FaphId { get; set; }
        public DateTime FaphStartdate { get; set; }
        public DateTime FaphEnddate { get; set; }
        public Decimal FaphLowPrice { get; set; }
        public Decimal FaphHighPrice { get; set; }
        public Decimal FaphRatePrice { get; set; }
        public Decimal FaphDiscount { get; set; }
        public Decimal FaphTaxRate { get; set; }
        public DateTime FaphModifiedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FaphFaciId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FaphUserId { get; set; }
    }
}
