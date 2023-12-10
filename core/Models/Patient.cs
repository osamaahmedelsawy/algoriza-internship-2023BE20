using core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Patient:BaseModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public int phone { get; set; }

        public string Image { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public List<Doctor> Doctors { get; set; }

        public string Password { get; set; }


    }
}
