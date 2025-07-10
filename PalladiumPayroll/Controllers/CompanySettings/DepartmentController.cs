using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.Services.Department;

namespace PalladiumPayroll.Controllers.Department
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("company/{companyId}")]
        public async Task<JsonResult> GetDepartmentsByCompanyId(long companyId)
        {
            return await _departmentService.GetDepartmentsByCompanyId(companyId);
        }

        [HttpPost]
        public async Task<JsonResult> CreateDepartment([FromBody] DepartmentRequestDTO request)
        {
            return await _departmentService.CreateDepartment(request);
        }

        [HttpPut("{departmentId}")]
        public async Task<JsonResult> EditDepartment(long departmentId, [FromBody] DepartmentRequestDTO request)
        {
            return await _departmentService.EditDepartment(departmentId, request);
        }

        [HttpDelete("{departmentId}")]
        public async Task<JsonResult> DeleteDepartment(long departmentId)
        {
            return await _departmentService.DeleteDepartment(departmentId);
        }
    }
}