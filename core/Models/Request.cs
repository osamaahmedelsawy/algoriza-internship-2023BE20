using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Request:BaseModel
    {
        public bool Completed { get; set; }
        public bool pendening { get; set; }
        public bool cancelled { get; set; }
        public DateTime RequestDate { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient patient { get; set; }

        public int DiscountId { get; set; }
        public Discount Discount { get; set; }

    }
}
