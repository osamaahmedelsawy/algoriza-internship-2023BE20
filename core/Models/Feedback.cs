using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Feedback:BaseModel
    {
        
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        public string Text { get; set; }
    }
}
