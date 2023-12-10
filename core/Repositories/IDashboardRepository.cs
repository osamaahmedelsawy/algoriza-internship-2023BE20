using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Repositories
{
    public interface IDashboardRepository
    {
        int GetNumberOfDoctors();
        int GetNumberOfPatients();
        Dictionary<string, int> GetNumberOfRequests();
        List<SpecializationRequestCount> GetTop5Specializations();
        List<DoctorRequestCount> GetTop10Doctors();

    }
}

