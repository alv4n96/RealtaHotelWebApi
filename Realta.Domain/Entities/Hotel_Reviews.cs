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
    [Table("Hotel_Reviews")]
    public class Hotel_Reviews
    {
        [Key]
        public int hore_id { get; set; }
        public string? hore_user_review { get; set; }
        [Range(1,5, ErrorMessage = "Value for Rating must be between {1} and {2}.")]
        public Byte hore_rating { get; set; }
        [AllowNull]
        public DateTime hore_created_on { get; set; } = DateTime.Now;
        public int hore_user_id { get; set; }
        public int hore_hotel_id { get; set; }
    }
}
