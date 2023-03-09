using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Realta.Domain.Entities
{
    [Table("Hotel_Reviews")]
    public class HotelReviews
    {
        [Key]
        public int HoreId { get; set; }
        public string? HoreUserReview { get; set; }
        [Range(1,5, ErrorMessage = "Value for Rating must be between {1} and {2}.")]
        public Byte HoreRating { get; set; }
        [AllowNull]
        public DateTime HoreCreatedOn { get; set; } = DateTime.Now;
        public int HoreUserId { get; set; }
        public int HoreHotelId { get; set; }
    }
}
