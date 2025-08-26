using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Employees;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.Employee
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("[action]")]
        public async Task<ActionResult> ExportEmployeeList([FromQuery] EmployeeFilterViewModel reqModel)
        {
            try
            {
                var fileBytes = await _employeeService.ExportEmployeeList(reqModel);
                return File(fileBytes, ContentTypes.Xlsx, "Employees.xlsx");
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

        [HttpGet("[action]")]
        public async Task<JsonResult> GetEmployeePersonalInfo(int employeeId)
        {
            try
            {
                return await _employeeService.GetEmployeePersonalInfo(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<JsonResult> GetEmployeePersonalInfoDropDown(int companyId)
        {
            try
            {
                return await _employeeService.GetEmployeePersonalInfoDropDown(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<JsonResult> SaveEmployeePersonalInfo(EmployeePersonalInformation reqModel)
        {
            try
            {
                return await _employeeService.SaveEmployeePersonalInfo(reqModel);
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
                    string.Format(ResponseMessages.Exception, ActionType.Deleting, $"{ResponseMessages.Employee} Directive Information", ex.Message));
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
        public async Task<ActionResult> GetPayrollTransactionList([FromQuery] TransactionReqModel reqModel)
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
        public async Task<ActionResult> SaveEmployeeTakeOnBalance(TransactionSaveModel reqModel)
        {
            try
            {
                return await _employeeService.SaveEmployeeTakeOnBalance(reqModel);
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

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteEmployeeTakeOnBalance(List<int> takeOnBalanceIds)
        {
            try
            {
                return await _employeeService.DeleteEmployeeTakeOnBalance(takeOnBalanceIds);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> SetTakeOnComplete(int employeeId)
        {
            try
            {
                return await _employeeService.SetTakeOnComplete(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }


        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteEmployeeLoan(int employeeLoanId)
        {
            try
            {
                if (employeeLoanId <= 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse("Invalid Loan Id.");
                }
                return await _employeeService.DeleteEmployeeLoan(employeeLoanId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.Exception, ActionType.Deleting, ResponseMessages.Employee + " Loan", ex.Message)
                );
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeLoanDetail(long employeeId)
        {
            try
            {
                return await _employeeService.GetEmployeeLoanDetail(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee + " Loan", ex.Message));
            }
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetGarnisheeDropdownData(long companyId)
        {
            try
            {
                return await _employeeService.GetGarnisheeDropdownData(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee + "  Garnishee DropList", ex.Message));
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetGarnisheeDetails(long employeeId)
        {
            try
            {
                return await _employeeService.GetGarnisheeDetails(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee + "  Garnishee", ex.Message));
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertGarnishee([FromBody] EmployeeGarnisheeRequest request)
        {
            try
            {
                return await _employeeService.UpsertGarnishee(request);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.Employee + " Garnishee", ex.Message)
                );
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetSavingsDetails(long employeeId)
        {
            try
            {
                return await _employeeService.GetSavingsDetails(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee + "  Savings", ex.Message));
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertSaving(EmployeeSavingsRequest request)
        {
            try
            {
                return await _employeeService.UpsertSaving(request);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.Employee + " Saving", ex.Message)
                );
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetTaxInformationDropdownData()
        {
            try
            {
                var response = await _employeeService.GetTaxInformationDropdownData();
                return HttpStatusCodeResponse.SuccessResponse(response, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTransfer, ActionType.Retrieved));
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<JsonResult> GetTaxInformation(int employeeId)
        {
            try
            {
                return await _employeeService.GetTaxInformation(employeeId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateTaxInformation(TaxInformation reqModel)
        {
            try
            {
                return await _employeeService.UpdateTaxInformation(reqModel);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeDocument(int employeeId)
        {
            try
            {
                return await _employeeService.GetEmployeeDocument(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadDocuments([FromForm] EmployeeDocumentUpload employeeDocument)
        {
            try
            {
                return await _employeeService.UploadDocuments(employeeDocument);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteDocuments([FromQuery] EmployeeDocumentDelete reqModel)
        {
            try
            {
                return await _employeeService.DeleteDocuments(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> DownloadDocument(string fileUrl)
        {
            try
            {
                return File(await _employeeService.DownloadDocument(fileUrl), ContentTypes.OctetStream, fileUrl.Split("\\").LastOrDefault());
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

        [HttpGet("[action]")]
        public async Task<ActionResult> GetPreviousService(int employeeId)
        {
            try
            {
                return await _employeeService.GetPreviousService(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
