namespace VezeetaProject.Dtos
{
    public class AppointmentDto
    {
            public int DoctorId { get; set; }
            public int PatientId { get; set; }
            public DateTime Date { get; set; }
            public DateTime Time { get; set; }

     }
}
