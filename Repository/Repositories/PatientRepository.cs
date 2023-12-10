using core.Models;
using core.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PatientRepository : IpatientRepository
    {
        private readonly VezeetaProjectDbContext _context;

        public PatientRepository(VezeetaProjectDbContext context) 
        {
            _context = context;
        }


        public bool RegisterPatient(Patient patientData)
        {
            try
            {
                _context.Patients.Add(patientData);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LoginPatient(User loginData)
        {
            try
            {
                var patient = _context.Patients.FirstOrDefault(p => p.Email == loginData.Email && p.Password == loginData.Password);
                return patient != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Doctor> SearchDoctors(int page, int pageSize, string search)
        {
            try
            {
                var query = _context.Doctors.AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(d => d.FullName.Contains(search) || d.specializations.Name.Contains(search));
                }

                return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Doctor>();
            }
        }

        public bool BookDoctor(Booking bookingData)
        {
            try
            {
                _context.Bookings.Add(bookingData);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Booking> GetPatientBookings(int patientId)
        {
            try
            {
                return _context.Bookings.Where(b => b.PatientId == patientId).ToList();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Booking>();
            }
        }

        public bool CancelBooking(int bookingId)
        {
            try
            {
                var booking = _context.Bookings.FirstOrDefault(b => b.Id == bookingId);
                if (booking != null)
                {
                    _context.Bookings.Remove(booking);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
