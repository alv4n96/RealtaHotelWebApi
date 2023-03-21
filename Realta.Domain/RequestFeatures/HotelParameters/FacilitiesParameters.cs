using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.RequestFeatures.HotelParameters
{

    public class FacilitiesParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
        public string OrderBy { get; set; } = "FaciName";

    }
}
