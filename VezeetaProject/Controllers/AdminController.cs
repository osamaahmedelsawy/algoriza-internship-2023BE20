using AutoMapper;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.data;
using Service.Services;
using VezeetaProject.Dtos;

namespace VezeetaProject.Controllers
{

    public class AdminController : BaseController
    {
        private readonly IDashboardService _dashboardService;
        private readonly IGenericRepository<Doctor> _doctorRepo;
        private readonly IGenericRepository<Patient> _patientRepo;
        private readonly IGenericRepository<Discount> _discountRepo;
        private readonly IMapper _mapper;

        public AdminController(IDashboardService dashboardService, IGenericRepository<Doctor> doctorRepo, IGenericRepository<Patient> PatientRepo, IGenericRepository<Discount> discountRepo, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _doctorRepo = doctorRepo;
            _patientRepo = PatientRepo;
            _discountRepo = discountRepo;
            _mapper = mapper;
        }
        //---------------------------Dashboard----------------------------------------//
        [HttpGet("Dashboard/statistics")]
        public ActionResult<DashboardStatistics> GetStatistics()
        {
            var statistics = _dashboardService.GetOverallStatistics();
            return Ok(statistics);
        }

        [HttpGet("Dashboard/statistics/timeframe")]
        public ActionResult<DashboardStatistics> GetStatisticsByTimeframe([FromQuery] string timeframe)
        {
            var statistics = _dashboardService.GetStatisticsByTimeframe(timeframe);
            return Ok(statistics);
        }
        //---------------------------Doctors----------------------------------------//
        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorToReturnDto>>> GetAll(int page = 1, int pageSize = 10, string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                
                var Doctors = await _doctorRepo.GetAllAsync(page, pageSize);
                var DoctorDto = _mapper.Map<IEnumerable<DoctorToReturnDto>>(Doctors);
                return Ok(DoctorDto);
            }
            else
            {
               
                var Doctors = await _doctorRepo.GetAllAsync(page, pageSize, search);
                var DoctorDto = _mapper.Map<IEnumerable<PatientToReturnDto>>(Doctors);
                return Ok(DoctorDto);
            }
        }
        [HttpGet("GetDoctorById/{id}")]
        public async Task<ActionResult<DoctorToReturnDto>> GetById(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Doctor, DoctorToReturnDto>(doctor));
        }
        [HttpPost("AddNewDoctor")]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorToReturnDto doctorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
              
                var doctor = _mapper.Map<Doctor>(doctorDto);

                _doctorRepo.AddAsync(doctor);
                
                var addedDoctorDto = _mapper.Map<DoctorToReturnDto>(doctor);
                return Ok(addedDoctorDto); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500);
            }
        }
        [HttpPut("EditDoctor/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DoctorToReturnDto doctorDto)
        {
            if (id == 0)
            {
                return BadRequest("ID does not match doctor ID.");
            }

            var existingDoctor = await _doctorRepo.GetByIdAsync(id);

            if (existingDoctor == null)
            {
                return NotFound("Doctor not found.");
            }

            
            
            existingDoctor.FullName = doctorDto.FullName;
            existingDoctor.Email = doctorDto.Email;
            existingDoctor.phone = doctorDto.phone;
            existingDoctor.specializations.Name = doctorDto.Specialization;
            existingDoctor.dateOfBirth = doctorDto.dateOfBirth;
            existingDoctor.ImageUrl = doctorDto.ImageUrl;
            

            try
            {
                await _doctorRepo.UpdateAsync(existingDoctor);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500);
            }
        }


        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            await _doctorRepo.RemoveAsync(doctor);
            return Ok(true);
        }

        //----------------------------------Patient-------------------------------------//
        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<IEnumerable<PatientToReturnDto>>> GetAllPatients(int page = 1, int pageSize = 10, string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                
                var patients = await _patientRepo.GetAllAsync(page, pageSize);
                var patientDto = _mapper.Map<IEnumerable<PatientToReturnDto>>(patients);
                return Ok(patientDto);
            }
            else
            {
                
                var patients = await _patientRepo.GetAllAsync(page, pageSize, search);
                var patientDto = _mapper.Map<IEnumerable<PatientToReturnDto>>(patients);
                return Ok(patientDto);
            }
        }

        [HttpGet("GetPatientById/{id}")]
        public async Task<ActionResult<PatientToReturnDto>> GetPatientById(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }
            var patientDto = _mapper.Map<PatientToReturnDto>(patient);
            return Ok(patientDto);
        }

        //------------------------Settings---------------------------------//

        [HttpPost("Add")]
        public async Task<ActionResult<Discount>> AddDiscount(Discount discount)
        {
            var addedDiscount = await _discountRepo.AddAsync(discount);
            return addedDiscount;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, Discount discount)
        {
            if (id != discount.Id)
            {
                return BadRequest("Id not found");
            }

            var existingDiscount = await _discountRepo.GetByIdAsync(id);
            if (existingDiscount == null)
            {
                return NotFound();
            }

            

            var updatedDiscount = await _discountRepo.UpdateAsync(discount);
            return Ok(updatedDiscount);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var result = await _discountRepo.RemoveAsync(new Discount { Id = id });
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }

}



    



