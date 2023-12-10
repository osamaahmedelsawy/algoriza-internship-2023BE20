using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.Enums;

namespace core.Models
{
    public class Discount:BaseModel
    {

        public string DiscountCode { get; set; }
        public DiscountType DiscountType { get; set; }
       
        public int Value { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

    }
}
