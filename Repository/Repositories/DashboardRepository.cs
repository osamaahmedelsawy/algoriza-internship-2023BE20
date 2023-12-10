using core.Models;
using core.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Repository.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {

        private readonly VezeetaProjectDbContext _context;

        public DashboardRepository(VezeetaProjectDbContext context)
        {
            _context = context;
        }

        public int GetNumberOfDoctors()
        {
            return _context.Doctors.Count();
        }

        public int GetNumberOfPatients()
        {
            return _context.Patients.Count();
        }

        public Dictionary<string, int> GetNumberOfRequests()
        {
            var numberOfRequests = new Dictionary<string, int>();

            numberOfRequests["TotalRequests"] = _context.Requests.Count();
            numberOfRequests["PendingRequests"] = _context.Requests.Count(r => r.pendening);
            numberOfRequests["CompletedRequests"] = _context.Requests.Count(r => r.Completed);
            numberOfRequests["CancelledRequests"] = _context.Requests.Count(r => r.cancelled);

            return numberOfRequests;
        }

        public List<SpecializationRequestCount> GetTop5Specializations()
        {
            var top5Specializations = _context.Specializations
                .GroupBy(s => s.Name)
                .Select(g => new SpecializationRequestCount
                {
                    SpecializationName = g.Key,
                    RequestCount = g.Count()
                })
                .OrderByDescending(x => x.RequestCount)
                .Take(5)
                .ToList();

            return top5Specializations;
        }

        public List<DoctorRequestCount> GetTop10Doctors()
        {
            var top10Doctors = _context.Doctors
                .Select(d => new DoctorRequestCount
                {
                    ImageUrl = d.ImageUrl,
                    FullName = d.FullName,
                    Specialization = d.specializations.Name, 
                    RequestCount = _context.Requests.Count(r => r.DoctorId == d.Id)
                })
                .OrderByDescending(x => x.RequestCount)
                .Take(10)
                .ToList();

            return top10Doctors;
        }
    }
    }
