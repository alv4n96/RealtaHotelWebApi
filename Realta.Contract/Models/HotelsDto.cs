using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class HotelsDto
    {
        [Required(ErrorMessage ="hotel id is required")]
        public int hotel_id { get; set; }

        [Required(ErrorMessage = "hotel name is required")] 
        public string? hotel_name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? hotel_description { get; set; }

        [Required(ErrorMessage = "hotel phone number is required")]
        [Phone]
        public string? hotel_phonenumber { get; set; }

        [AllowNull]
        public Int16 hotel_rating_star { get; set; }
        [Required(ErrorMessage = "hotel status is required")]
        public string? hotel_status { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? hotel_reason_status { get; set; }

        [AllowNull]
        public DateTime hotel_modified_date { get; set; } = DateTime.Now;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Required(ErrorMessage = "hotel address is required")]
        public int? hotel_addr_id { get; set; }
    }
}
