using core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Booking:BaseModel
    {

        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient patient { get; set; }
        
        public string DiscountCode { get; set; }
        public Discount Discount { get; set; }

        public Days Day { get; set; }

        public DateTime time { get; set; }

        public decimal price { get; set; }
        public decimal FinalPrice { get; set; }

        public string Status { get; set; }
    }
}
