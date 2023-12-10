using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class TopDoctor
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Specialize { get; set; }
        public int NumOfRequests { get; set; }
    }
}
