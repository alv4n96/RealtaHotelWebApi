using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Hotels")]
    public class Hotels
    {
        [Key]
        public int hotelId { get; set; }
        public string? hotelName { get; set; }
        [AllowNull]
        public string? hotelDescription { get; set; } = string.Empty;
        public bool hotelStatus { get; set; }
        [AllowNull]
        public string? hotelReasonStatus { get; set; } = string.Empty;
        [AllowNull]
        public Int16 hotelRatingStar { get; set; }
        [Phone]
        public string? hotelPhonenumber { get; set; }
        [AllowNull]
        public DateTime hotelModifiedDate { get; set; } = DateTime.Now;
        public int? hotelAddrId { get; set; } 
    }
}
