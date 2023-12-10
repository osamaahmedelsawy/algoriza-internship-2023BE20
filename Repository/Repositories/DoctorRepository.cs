using core.Models;
using core.Repositories;
using Repository.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly VezeetaProjectDbContext _context; 
    public DoctorRepository(VezeetaProjectDbContext context)
    {
        _context = context;
    }
    public bool AddAppointment(Appointment appointmentData)
    {
        var newAppointment = new Appointment
        {
            DoctorId = appointmentData.DoctorId,
            PatientId = appointmentData.PatientId,
            date = appointmentData.date,
            time = appointmentData.time,
            Doctor = appointmentData.Doctor,
            Patient = appointmentData.Patient
        };

        _context.Appointments.Add(newAppointment);
        _context.SaveChanges();
        return true;
    }

   
    public Doctor AuthenticateDoctor(string email, string password)
    {
        return _context.Doctors.FirstOrDefault(d => d.Email == email && d.password == password);
    }

    public bool ConfirmCheckUp(int appointmentId)
    {
        var appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);

        if (appointment != null)
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public bool DeleteAppointment(int appointmentId)
    {
        var appointmentToDelete = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);

        if (appointmentToDelete == null)
        {
            return false;
        }

        _context.Appointments.Remove(appointmentToDelete);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<Appointment> GetAllAppointmentsForDoctor(int doctorId, DateTime searchDate, int pageSize, int pageNumber)
    {
        return _context.Appointments
            .Where(a => a.DoctorId == doctorId && a.date.Date == searchDate.Date)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Doctor GetDoctorById(int doctorId)
    {
        return _context.Doctors.FirstOrDefault(d => d.Id == doctorId);
    }

    public bool UpdateAppointment(int appointmentId, Appointment updatedAppointmentData)
    {
        var existingAppointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);

        if (existingAppointment == null)
        {
            return false;
        }

        existingAppointment.date = updatedAppointmentData.date;
        existingAppointment.time = updatedAppointmentData.time;
        existingAppointment.Patient = updatedAppointmentData.Patient;
        existingAppointment.Doctor = updatedAppointmentData.Doctor;
        existingAppointment.PatientId = updatedAppointmentData.PatientId;
        existingAppointment.DoctorId = updatedAppointmentData.DoctorId;

        _context.SaveChanges();
        return true;
    }

    
}
