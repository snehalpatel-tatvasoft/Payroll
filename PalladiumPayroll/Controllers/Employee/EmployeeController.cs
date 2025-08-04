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

        [HttpPost("[action]")]
        public async Task<ActionResult> AddDirective(DirectiveRequest reqModel)
        {
            try
            {
                return await _employeeService.AddDirective(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.EmployeeTransfer, ex.Message));
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetDirectivesByEmployeeId(int employeeId)
        {
            var result = await _employeeService.GetDirectivesByEmployeeId(employeeId);
            return HttpStatusCodeResponse.SuccessResponse(
                result,
                string.Format(ResponseMessages.Success, $"{ResponseMessages.Employee} Directive Information", ActionType.Retrieved)
            );
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateDirective(long directiveId, DirectiveRequest reqModel)
        {
            try
            {
                return await _employeeService.UpdateDirective(directiveId, reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.Exception, ActionType.Updating, $"{ResponseMessages.Employee} Directive Information", ex.Message));
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteDirective(long directiveId)
        {
            try
            {
                return await _employeeService.DeleteDirective(directiveId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.Exception, ActionType.Deleting,  $"{ResponseMessages.Employee} Directive Information", ex.Message));
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
        public async Task<ActionResult> GetPayrollTransactionList([FromQuery]TransactionReqModel reqModel)
        {
            try
            {
                return await _employeeService.GetPayrollTransactionList(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SaveEmployeeTransaction(TransactionSaveModel reqModel)
        {
            try
            {
                return await _employeeService.SaveEmployeeTransaction(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeTakeOnBalance(int employeeId, int allowanceType)
        {
            try
            {
                return await _employeeService.GetEmployeeTakeOnBalance(employeeId, allowanceType);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

    }
}
