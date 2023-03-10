using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Realta.Contract.Models.v1
{
    public class FacilityPhotoGroupDto
    {
        [Required]
        public FacilityPhotosDto? FacilityPhotos { get; set; }

        public List<IFormFile>? AllPhotos { get; set; }
    }
}
