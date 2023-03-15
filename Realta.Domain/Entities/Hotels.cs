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
        public string? HotelDescription { get; set; }
        public bool HotelStatus { get; set; }
        [AllowNull]
        public string? HotelReasonStatus { get; set; } 
        [AllowNull]
        public decimal HotelRatingStar { get; set; }
        [Phone]
        public string? HotelPhonenumber { get; set; }
        [AllowNull]
        public DateTime HotelModifiedDate { get; set; }
        public int HotelAddrId { get; set; } 
        public string? HotelAddrDescription { get; set; }

    }
}
