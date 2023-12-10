using core.Models;
using core.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.data;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DashboardServices : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly VezeetaProjectDbContext _context;

        public DashboardServices(IDashboardRepository dashboardRepository, VezeetaProjectDbContext context)
        {
            _dashboardRepository = dashboardRepository;
            _context = context;
        }

        public DashboardStatistics GetOverallStatistics()
        {
            var requestsDictionary = _dashboardRepository.GetNumberOfRequests();
            int totalRequests = requestsDictionary.ContainsKey("TotalRequests") ? requestsDictionary["TotalRequests"] : 0;


            var overallStats = new DashboardStatistics();

            overallStats.NumOfDoctors = _dashboardRepository.GetNumberOfDoctors();
            overallStats.NumOfPatients = _dashboardRepository.GetNumberOfPatients();
            overallStats.NumOfRequests = totalRequests;
            overallStats.Top5Specializations = _dashboardRepository.GetTop5Specializations();
            overallStats.Top10Doctors = _dashboardRepository.GetTop10Doctors();


            return overallStats;
        }



        public DashboardStatistics GetStatisticsByTimeframe(string timeframe)
        {
            DateTime startDate;
            DateTime endDate = DateTime.Now;

            switch (timeframe.ToLower())
            {
                case "last24hours":
                    startDate = DateTime.Now.AddHours(-24);
                    break;
                case "lastweek":
                    startDate = DateTime.Now.AddDays(-7);
                    break;
                case "lastmonth":
                    startDate = DateTime.Now.AddMonths(-1);
                    break;
                case "lastyear":
                    startDate = DateTime.Now.AddYears(-1);
                    break;
                default:
                    startDate = DateTime.MinValue; 
                    break;
            }

            
            var filteredRequests = _context.Requests
                .Where(r => r.RequestDate >= startDate && r.RequestDate <= endDate)
                .ToList(); 
            int numOfRequests = filteredRequests.Count;

            var top5Specializations = filteredRequests
                .GroupBy(r => r.Doctor.specializations)
                .Select(g => new { Specialization = g.Key, RequestCount = g.Count() })
                .OrderByDescending(x => x.RequestCount)
                .Take(5)
                .ToList();

            
            var top10Doctors = filteredRequests
                .GroupBy(r => r.Doctor)
                .Select(g => new
                {
                    Doctor = g.Key,
                    RequestCount = g.Count()
                })
                .OrderByDescending(x => x.RequestCount)
                .Take(10)
                .Select(x => new
                {
                    ImageUrl = x.Doctor.ImageUrl,
                    FullName = x.Doctor.FullName,
                    Specialization = x.Doctor.specializations,
                    RequestCount = x.RequestCount
                })
                .ToList();


            List<SpecializationRequestCount> convertedTop5Specializations = top5Specializations
                .Select(s => new SpecializationRequestCount
                {
                    SpecializationName = s.Specialization.Name, 
                    RequestCount = s.RequestCount
                })
                .ToList();

            List<DoctorRequestCount> convertedTop10Doctors = top10Doctors
                .Select(d => new DoctorRequestCount
                {
                    ImageUrl = d.ImageUrl,
                    FullName = d.FullName,
                    Specialization = d.Specialization.Name, 
                    RequestCount = d.RequestCount
                })
                .ToList();
            
            var statistics = new DashboardStatistics
            {
                NumOfDoctors = filteredRequests.Select(r => r.DoctorId).Distinct().Count(),
                NumOfPatients = filteredRequests.Select(r => r.PatientId).Distinct().Count(),
                NumOfRequests = numOfRequests,
                Top5Specializations = convertedTop5Specializations,
                Top10Doctors = convertedTop10Doctors
            };

            return statistics;
        }
    }
}

