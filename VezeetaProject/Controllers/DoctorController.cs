using AutoMapper;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.data;
using Repository.Repositories;
using VezeetaProject.Dtos;

namespace VezeetaProject.Controllers
{ 

    public class DoctorsController : BaseController
    {
            private readonly VezeetaProjectDbContext _context;
           private readonly IDoctorRepository _doctorRepository;
            private readonly IMapper _mapper;

        public DoctorsController(VezeetaProjectDbContext context, IDoctorRepository doctorRepository, IMapper mapper)
        {
            _context = context;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }



    [HttpGet("{GetDoctorById}")]
    public IActionResult GetDoctorById(int doctorId)
    {
        var doctor = _doctorRepository.GetDoctorById(doctorId);

        if (doctor == null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }

    [HttpGet("{doctorId}/appointments")]
    public IActionResult GetDoctorAppointments(int doctorId, DateTime searchDate, int pageSize = 10, int pageNumber = 1)
    {
        var appointments = _doctorRepository.GetAllAppointmentsForDoctor(doctorId, searchDate, pageSize, pageNumber);

        return Ok(appointments);
    }

    [HttpPost("AddAppointments")]
    public IActionResult AddAppointment([FromBody] Appointment appointmentData)
    {
        var result = _doctorRepository.AddAppointment(appointmentData);

        if (result)
        {
            return Ok("Appointment added successfully");
        }

        return BadRequest("Failed to add appointment");
    }

    [HttpPut("appointments/{appointmentId}")]
    public IActionResult UpdateAppointment(int appointmentId, [FromBody] Appointment updatedAppointmentData)
    {
        var result = _doctorRepository.UpdateAppointment(appointmentId, updatedAppointmentData);

        if (result)
        {
            return Ok("Appointment updated successfully");
        }

        return BadRequest("Failed to update appointment");
    }

    [HttpDelete("appointments/{appointmentId}")]
    public IActionResult DeleteAppointment(int appointmentId)
    {
        var result = _doctorRepository.DeleteAppointment(appointmentId);

        if (result)
        {
            return Ok("Appointment deleted successfully");
        }

        return BadRequest("Failed to delete appointment");
    }

    [HttpPost("confirm-checkup/{appointmentId}")]
    public IActionResult ConfirmCheckUp(int appointmentId)
    {
        var result = _doctorRepository.ConfirmCheckUp(appointmentId);

        if (result)
        {
            return Ok("Check-up confirmed successfully");
        }

        return BadRequest("Failed to confirm check-up");
    }

    private bool IsValidEmail(string email)
    {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var emailRegex = new System.Text.RegularExpressions.Regex(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@"
                                    + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|"
                                    + @"(([a-zA-Z\-0-9]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                return emailRegex.IsMatch(email);
            }
            catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
            {
                return false;
            }

        }
    private bool IsValidPassword(string password)
    {
        const int minLength = 8;

        
        if (password.Length < minLength)
        {
            return false;
        }
        bool hasUpperCase = password.Any(char.IsUpper);
        bool hasLowerCase = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch));

        
        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
    }
}


    
}



