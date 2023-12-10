using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Repositories
{
    public interface IpatientRepository
    {
        bool RegisterPatient(Patient patientData);
        bool LoginPatient(User loginData);
        IEnumerable<Doctor> SearchDoctors(int page, int pageSize, string search);
        bool BookDoctor(Booking bookingData);
        IEnumerable<Booking> GetPatientBookings(int patientId);
        bool CancelBooking(int bookingId);
    }
}
