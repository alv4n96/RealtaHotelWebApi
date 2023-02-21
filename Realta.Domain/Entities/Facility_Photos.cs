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
    public class Facility_Photos
    {
        [Key]
        public int fapho_id { get; set; }
        [AllowNull]
        public string fapho_thumbnail_filename { get; set; }
        [AllowNull]
        public string fapho_photo_filename { get; set; }
        [AllowNull]
        public Boolean fapho_primary { get; set; }
        [AllowNull]
        public string fapho_url { get; set; }
        [AllowNull]
        public DateTime fapho_modified_date { get; set; }
        public int fapho_faci_id { get; set; }
    }
}
