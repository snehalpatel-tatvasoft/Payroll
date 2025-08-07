using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Employees;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeFilters(int companyId)
        {
            try
            {
                return await _employeeService.GetEmployeeFilters(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeList([FromQuery] EmployeeFilterViewModel reqModel)
        {
            try
            {
                return await _employeeService.GetEmployeeList(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                return await _employeeService.DeleteEmployee(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeePaymentDetail(int employeeId)
        {
            try
            {
                return await _employeeService.GetEmployeePaymentDetail(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> EmployeePaymentDetailSave(EmployeePaymentDetail reqModel)
        {
            try
            {
                return await _employeeService.EmployeePaymentDetailSave(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeWorkDropDown(int companyId)
        {
            try
            {
                return await _employeeService.GetEmployeeWorkDropDown(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeWorkInformation(int employeeId)
        {
            try
            {
                return await _employeeService.GetEmployeeWorkInformation(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> EmployeeWorkInfoSave(EmployeeWorkInformation reqModel)
        {
            try
            {
                return await _employeeService.EmployeeWorkInfoSave(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeWorkOrganizationalDropdownData(long companyId)
        {
            try
            {
                return await _employeeService.GetEmployeeWorkOrganizationalDropdownData(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<JsonResult> DeleteWorkOrganizationalDropdownItem(int id, int type)
        {
            try
            {
                return await _employeeService.DeleteWorkOrganizationalDropdownItem(id, type);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<JsonResult> AddWorkOrganizationalDropdownItem(WorkOrgnizationItem reqItem)
        {
            try
            {
                return await _employeeService.AddWorkOrganizationalDropdownItem(reqItem);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeWorkOrganizationalData(long employeeId)
        {
            try
            {
                return await _employeeService.GetEmployeeWorkOrganizationalData(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SaveEmployeeWorkOrganizationalData(EmployeeOrgnizationalModel reqModel)
        {
            try
            {
                return await _employeeService.SaveEmployeeWorkOrganizationalData(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeTimeSheetSetup(long employeeId)
        {
            try
            {
                return await _employeeService.GetEmployeeTimeSheetSetup(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SaveEmployeeTimeSheetSetup(TimeSheetSetup reqModel)
        {
            try
            {
                return await _employeeService.SaveEmployeeTimeSheetSetup(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeByEmployeeId(long employeeId, long companyId)
        {
            try
            {
                return await _employeeService.GetEmployeeByEmployeeId(employeeId, companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetSecondApprovalEmployeeListByCompanyId(long companyId)
        {
            try
            {
                return await _employeeService.GetSecondApprovalEmployeeListByCompanyId(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateEmployeeSelfService([FromBody] UpdateEmployeeSelfServiceModel model)
        {
            try
            {
                if (model == null || model.FunctionalityList == null || !model.FunctionalityList.Any())
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InvalidOrMissingRequestParameters);
                }
                return await _employeeService.UpdateEmployeeSelfService(model);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAccessRolesByCompanyId(long companyId)
        {
            try
            {
                return await _employeeService.GetAccessRolesByCompanyId(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
