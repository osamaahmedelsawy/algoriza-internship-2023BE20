using core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Doctor:BaseModel
    {
        public string FullName { get; set; }
        
        public string Email { get; set; }

        public int phone { get; set; }

        public DateTime dateOfBirth { get; set; }

        public int SpecializationsId { get; set; }
        public Specializations specializations { get; set; }

        public string ImageUrl { get; set; }

        public List<Patient> patients { get; set; }
        public Gender Gender { get; set; }
        
        [DataType(DataType.Password)]
       public string password { get; set; }
        


    }
}
