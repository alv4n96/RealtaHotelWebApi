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
    public class FacilityPhotosDto
    {
        [Required(ErrorMessage = "facility photos id is required")]
        public int fapho_id { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(@"^.*\.(jpg|jpeg|png)$", ErrorMessage = "Photo files must be JPEG or PNG files")]
        public string fapho_thumbnail_filename { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(@"^.*\.(jpg|jpeg|png)$", ErrorMessage = "Photo files must be JPEG or PNG files")]
        public string fapho_photo_filename { get; set; }
        [AllowNull]
        public Boolean fapho_primary { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(@"^(http|https)://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$", ErrorMessage = "URL Not valid.")]
        public string fapho_url { get; set; }
        [AllowNull]
        public DateTime fapho_modified_date { get; set; }
        [Required(ErrorMessage = "facility id is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int fapho_faci_id { get; set; }
    }
}
