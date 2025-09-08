using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Department;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

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

        [HttpGet("[action]")]
        public async Task<JsonResult> GetDepartmentsByCompanyId(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
                }

                return await _departmentService.GetDepartmentsByCompanyId(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.Department));
            }
        }

        [HttpPost("[action]")]
        public async Task<JsonResult> CreateDepartment([FromBody] DepartmentRequestDTO request)
        {
            try
            {
                return await _departmentService.CreateDepartment(request);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.Department)
                );
            }
        }

        [HttpPut("[action]")]
        public async Task<JsonResult> EditDepartment(long departmentId, [FromBody] DepartmentRequestDTO request)
        {
            try
            {
                return await _departmentService.EditDepartment(departmentId, request);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.Department));
            }
        }

        [HttpDelete("[action]")]
        public async Task<JsonResult> DeleteDepartment(long departmentId,long? employeeId)
        {
            try
            {
                 return await _departmentService.DeleteDepartment(departmentId, employeeId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.Department));
            }
        }
    }
}