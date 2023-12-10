using core.Enums;
using core.Models;

namespace VezeetaProject.Dtos
{
    public class PatientToReturnDto
    {
        public int Id { get; set; }
       
        public string FullName { get; set; }

        public string Email { get; set; }

        public int phone { get; set; }

        public string Image { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Doctor { get; set; }

    }
}
