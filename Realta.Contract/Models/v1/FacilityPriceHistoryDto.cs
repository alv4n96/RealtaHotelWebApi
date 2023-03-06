using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Realta.Contract.Models.v1
{
    public class FacilityPriceHistoryDto
    {
        public int FaphId { get; set; }
        public DateTime FaphStartdate { get; set; }
        public DateTime FaphEnddate { get; set; }
        public decimal FaphLowPrice { get; set; }
        public decimal FaphHighPrice { get; set; }
        public decimal FaphRatePrice { get; set; }
        public decimal FaphDiscount { get; set; }
        public decimal FaphTaxRate { get; set; }
        public DateTime FaphModifiedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FaphFaciId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FaphUserId { get; set; }
    }
}
