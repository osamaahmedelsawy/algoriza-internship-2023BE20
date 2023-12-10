using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Repositories
{
    public interface IDoctorRepository
    {
        Doctor GetDoctorById(int doctorId);
        IEnumerable<Appointment> GetAllAppointmentsForDoctor(int doctorId, DateTime searchDate, int pageSize, int pageNumber);
        bool ConfirmCheckUp(int appointmentId);
        bool AddAppointment(Appointment appointmentData);
        bool UpdateAppointment(int appointmentId, Appointment updatedAppointmentData);
        bool DeleteAppointment(int appointmentId);
        Doctor AuthenticateDoctor(string email, string password);

    }
}
