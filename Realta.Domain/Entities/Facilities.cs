using Realta.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Realta.Domain.Entities
{
    [Table("Facilities")]
    public class Facilities
    {
        [Key]
        public int FaciId { get; set; }
        public string? FaciName { get; set; }
        [AllowNull]
        public string? FaciDescription { get; set; }
        [AllowNull]
        public int FaciMaxNumber { get; set; }
        [AllowNull]
        public string? FaciMeasureUnit { get; set; }
        public string? FaciRoomNumber { get; set; }
        public DateTime FaciStartdate { get; set; }
        public DateTime FaciEndDate { get; set; }
        public Decimal FaciLowPrice { get; set; }
        public Decimal FaciHighPrice { get; set; }
        public Decimal FaciRatePrice { get; set; }
        public Int16 FaciExposePrice { get; set; }
        [AllowNull]
        public Decimal FaciDiscount { get; set; }
        [AllowNull]
        public Decimal  FaciTaxRate { get; set; }
        [AllowNull]
        public DateTime FaciModifiedDate { get; set; }
        public int FaciCagroId { get; set; }
        public int FaciHotelId { get; set; }
        public int FaciUserId { get; set; }
    }
}
