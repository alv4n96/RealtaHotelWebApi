using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class HotelsDto
    {
        [Required]
        public int hotel_id { get; set; }
        [Required]
        public string? hotel_name { get; set; }
        public string? hotel_description { get; set; } 
        public Int16 hotel_rating_star { get; set; } 
        [Required]
        public string? hotel_phonenumber { get; set; }
        public DateTime hotel_modified_date { get; set; } = DateTime.Now;
        [Required]
        public int hotel_addr_id { get; set; }
    }
}
