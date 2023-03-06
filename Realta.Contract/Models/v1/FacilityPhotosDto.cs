using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Realta.Contract.Models.v1
{
    public class FacilityPhotosDto
    {
        [Required(ErrorMessage = "facility photos id is required")]
        public int FaphoId { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(@"^.*\.(jpg|jpeg|png)$", ErrorMessage = "Photo files must be JPEG or PNG files")]
        public string FaphoThumbnailFilename { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(@"^.*\.(jpg|jpeg|png)$", ErrorMessage = "Photo files must be JPEG or PNG files")]
        public string FaphoPhotoFilename { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FaphoOriginalFilename { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public short FaphoFileSize { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FaphoFileType { get; set; }
        [AllowNull]
        public bool FaphoPrimary { get; set; }
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [RegularExpression(@"^(http|https)://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$", ErrorMessage = "URL Not valid.")]
        public string FaphoUrl { get; set; }
        [AllowNull]
        public DateTime FaphoModifiedDate { get; set; }
        [Required(ErrorMessage = "facility id is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FaphoFaciId { get; set; }
    }
}
