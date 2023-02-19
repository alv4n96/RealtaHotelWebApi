using Realta.Contract.Models.Hotels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class HotelReviewsDto
    {
        [Required(ErrorMessage = "hotel review id is required")]
        public int hore_id { get; set; }
        [Required(ErrorMessage = "user review is required")]
        public string? hore_user_review { get; set; }
        [Required(ErrorMessage = "user id is required")]
        public int hore_user_id { get; set; }
        [Required(ErrorMessage = "hotel id is required")]
        public int hore_hotel_id { get; set; }
        [Required(ErrorMessage = "rating is required")]
        [Range(1, 5, ErrorMessage = "Value for Rating must be between {1} and {2}.")]
        public Byte hore_rating { get; set; } = 5;
        [AllowNull]
        public DateTime hore_created_on { get; set; } = DateTime.Now;
    }
}
