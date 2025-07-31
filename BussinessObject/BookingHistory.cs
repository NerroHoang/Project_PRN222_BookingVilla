using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class BookingHistory : BookingOnline
    {
        public string NameVilla { get; set; }
        public List<string> ServicesIncluded { get; set; }
        public string Status { get; set; }
    }

}

