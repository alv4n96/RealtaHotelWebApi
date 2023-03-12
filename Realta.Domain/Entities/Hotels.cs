using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Realta.Domain.Entities
{
    [Table("Hotels")]
    public class Hotels
    {
        [Key]
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        [AllowNull]
        public string? HotelDescription { get; set; } = string.Empty;
        public bool HotelStatus { get; set; }
        [AllowNull]
        public string? HotelReasonStatus { get; set; } = string.Empty;
        [AllowNull]
        public decimal HotelRatingStar { get; set; }
        [Phone]
        public string? HotelPhonenumber { get; set; }
        [AllowNull]
        public DateTime HotelModifiedDate { get; set; } = DateTime.Now;
        public int? HotelAddrId { get; set; } 

    }
}
