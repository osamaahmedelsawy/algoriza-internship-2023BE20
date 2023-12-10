using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class DoctorRequestCount
    {
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public int RequestCount { get; set; }
    }
}
