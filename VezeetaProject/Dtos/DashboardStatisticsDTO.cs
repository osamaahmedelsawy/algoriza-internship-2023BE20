namespace VezeetaProject.Dtos
{
    public class DashboardStatisticsDTO
    {
        public int NumOfDoctors { get; set; }
        public int NumOfPations { get; set; }
        public DashboardRequestStatsDTO NumOfRequests { get; set; }
        public List<SpecializationStatsDTO> TopSpecializations { get; set; }
        public List<TopDoctorDTO> TopDoctors { get; set; }
    }
}

