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
        public int hotel_id { get; set; }
        public string? hotel_name { get; set; }
        [AllowNull]
        public string? hotel_description { get; set; } = string.Empty;
        public bool hotel_status { get; set; }
        [AllowNull]
        public string? hotel_reason_status { get; set; } = string.Empty;
        [AllowNull]
        public Int16 hotel_rating_star { get; set; }
        [Phone]
        public string? hotel_phonenumber { get; set; }
        [AllowNull]
        public DateTime hotel_modified_date { get; set; } = DateTime.Now;
        public int hotel_addr_id { get; set; } 
    }
}
