using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Services.Home
{
    public interface IHomeService
    {
        Task<List<Employee>> GetAllEmployeeList(int employeeId);
        Task<Dashboard> GetDashboardData();
    }
}
