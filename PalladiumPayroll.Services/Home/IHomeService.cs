using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;

namespace PalladiumPayroll.Services.Home
{
    public interface IHomeService
    {
        Task<List<Employee>> GetAllEmployeeList(int employeeId);
    }
}
