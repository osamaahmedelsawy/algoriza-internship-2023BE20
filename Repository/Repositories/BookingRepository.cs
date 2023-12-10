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
    public class BookingRepository : IBookingRepository
    {
        private readonly VezeetaProjectDbContext _context;

        public BookingRepository(VezeetaProjectDbContext context)
        {
            _context = context;
        }

        public bool CreateBooking(Booking bookingData)
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

        public IEnumerable<Booking> GetBookingsByPatient(int patientId)
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
