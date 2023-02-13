using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    internal class Hotels
    {
        public int hotel_id { get; set; }
        public string hotel_name { get; set; }
        public string hotel_description { get; set; }
        public UInt16 hotel_rating_star { get; set; }
        public string hotel_phonenumber { get; set; }
        public DateTime hotel_modified_date { get; set; } = DateTime.Now;
        public int hotel_addr_id { get; set; } 
    }
}
