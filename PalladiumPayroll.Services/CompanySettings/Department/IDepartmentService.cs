using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.Repositories.Department;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.Department
{
    public interface IDepartmentService
    {
        Task<JsonResult> GetDepartmentsByCompanyId(long companyId);
        Task<JsonResult> CreateDepartment(CreateDepartmentRequestDto request);
        Task<JsonResult> EditDepartment(long departmentId, EditDepartmentRequestDto request);
        Task<JsonResult> DeleteDepartment(long departmentId);
    }
}

