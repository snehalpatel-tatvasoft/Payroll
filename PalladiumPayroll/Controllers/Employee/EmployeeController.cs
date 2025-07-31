using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Employees;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

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
        public async Task<ActionResult> GetCasualWageInformation(int employeeId)
        {
            try
            {
                return await _employeeService.GetCasualWageInformation(employeeId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateCasualWageInformation(CasualWageInformation reqModel)
        {
            try
            {
                return await _employeeService.UpdateCasualWageInformation(reqModel);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> GetTransactionTypesDropdownData(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
                }

                var response = await _employeeService.GetTransactionTypesDropdownData(companyId);
                return HttpStatusCodeResponse.SuccessResponse(response, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTransfer, ActionType.Retrieved));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTransfer, ex.Message));
            }
        }

    }
}
