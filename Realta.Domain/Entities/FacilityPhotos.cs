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
    [Table("Facility_Photos")]
    public class FacilityPhotos
    {
        [Key]
        public int FaphoId { get; set; }
        [AllowNull]
        public string FaphoThumbnailFilename { get; set; }
        [AllowNull]
        public string FaphoPhotoFilename { get; set; }
        public string? FaphoOriginalFilename { get; set; }
        [AllowNull]
        public short FaphoFileSize { get; set; }
        public string? FaphoFileType { get; set; }
        [AllowNull]
        public Boolean FaphoPrimary { get; set; }
        [AllowNull]
        public string FaphoUrl { get; set; }
        [AllowNull]
        public DateTime FaphoModifiedDate { get; set; }
        public int FaphoFaciId { get; set; }
    }
}
