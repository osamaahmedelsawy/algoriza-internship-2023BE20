using core.Enums;
using core.Models;

namespace VezeetaProject.Dtos
{
    public class DoctorToReturnDto
    {
        public int Id { get; set; }
       
        public string FullName { get; set; }

        public string Email { get; set; }

        public int phone { get; set; }

        public string Specialization { get; set; }

        public DateTime dateOfBirth { get; set; }

        public string ImageUrl { get; set; }
        public string Gender { get; set; }


    } 

}




