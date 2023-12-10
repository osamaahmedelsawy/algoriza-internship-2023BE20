using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IDashboardService
    {
        DashboardStatistics GetOverallStatistics();
        DashboardStatistics GetStatisticsByTimeframe(string timeframe);

    }
}
