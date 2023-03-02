using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Facility_Price_History")]
    public class FacilityPriceHistory
    {
        [Key]
        public int FaphId {get; set;}
        public DateTime FaphStartdate { get; set;}
        public DateTime FaphEnddate { get; set;}
        public Decimal   FaphLowPrice { get; set; }
        public Decimal FaphHighPrice { get; set;}
        public Decimal   FaphRatePrice { get; set;}
        public Decimal FaphDiscount   { get; set;}
        public Decimal  FaphTaxRate { get; set;}
        public DateTime FaphModifiedDate { get; set;}
        public int FaphFaciId { get; set; }
        public int FaphUserId { get; set;}
    }
}
