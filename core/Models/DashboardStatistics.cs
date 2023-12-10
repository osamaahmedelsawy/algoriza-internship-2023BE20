using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class DashboardStatistics
    {
        public int NumOfDoctors { get; set; }
        public int NumOfPatients { get; set; }
        public int NumOfRequests { get; set; }
        public List<SpecializationRequestCount> Top5Specializations { get; set; }
        public List<DoctorRequestCount> Top10Doctors { get; set; }
    }
}
