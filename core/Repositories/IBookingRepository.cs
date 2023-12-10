using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Repositories
{
  
     public interface IBookingRepository
        {
            bool CreateBooking(Booking bookingData);
            IEnumerable<Booking> GetBookingsByPatient(int patientId);
            bool CancelBooking(int bookingId);
        }
    }

