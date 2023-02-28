using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class FacilityPhotoGroupDto
    {
        [Required]
        public FacilityPhotosDto? FacilityPhotos { get; set; }

        public List<IFormFile>? AllPhotos { get; set; }
    }
}
