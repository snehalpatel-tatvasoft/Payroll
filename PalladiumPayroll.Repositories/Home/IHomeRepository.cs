using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.Miscellaneous;

namespace PalladiumPayroll.Repositories.Home
{
    public interface IHomeRepository
    {
        Task<List<Employee>> GetAllEmployeeList(int employeeId);
    }
}
