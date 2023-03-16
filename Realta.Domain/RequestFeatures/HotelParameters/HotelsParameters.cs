using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.RequestFeatures.HotelParameters
{
    public class HotelsParameters : RequestParameters
    {

        public uint Unavailable { get; set; }
        public uint Available { get; set; } = int.MaxValue;

        public bool ValidateStatus => Available > Unavailable;
        public string? SearchTerm { get; set; }
    }

}
