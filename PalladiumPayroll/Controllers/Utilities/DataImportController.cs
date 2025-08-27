using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Utilities.DataImport;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.Utilities;

[ApiController]
[Route("api/[controller]")]
public class DataImportController : ControllerBase
{
    private readonly IDataImportService _dataImportService;

    public DataImportController(IDataImportService dataImportService)
    {
        _dataImportService = dataImportService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> GetPayrollProcessingTransactions([FromQuery] PayrollTransactionFilterViewModel reqModel)
    {
        try
        {
            return await _dataImportService.GetPayrollProcessingTransactionsByCompany(reqModel);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.Transaction)
            );
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request)
    {
        try
        {
            return await _dataImportService.AddImportYearToDateTemplate(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.YearToDateTemplate)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetDropDownForYearToDateTemplate(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }
            return await _dataImportService.GetDropDownForYearToDateTemplate(companyId);
        }
        catch (Exception)
        {
             return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.YearToDateTemplate + "Dropdown")
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetTransactionForExcelGenerate(int templateId)
    {
        try
        {
            return await _dataImportService.GetTransactionForExcelGenerate(templateId);
        }
        catch (Exception)
        {
             return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.Transaction)
            );
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ImportYTDRecord([FromBody] ImportYearToDateRecordRequestDTO request)
    {
        try
        {
            return await _dataImportService.ImportYTDRecord(request);
        }
        catch (Exception)
        {
           return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Importing, ResponseMessages.YearToDateTemplate)
            );
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ImportWorkInformation([FromBody] WorkInformationImportRequestDTO request)
    {
        try
        {
            return await _dataImportService.ImportWorkInformation(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Importing, ResponseMessages.WorkInformation)
            );
        }
    }


    [HttpPost("[action]")]
    public async Task<ActionResult> EmployeeMasterfileImport([FromBody] EmployeeMasterImportRequestDTO request)
    {
        try
        {
            JsonResult? res = await _dataImportService.EmployeeMasterfileImport(request);
            return res;
        }
        catch (Exception)
        {
             return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Importing, ResponseMessages.EmployeeMasterfile)
            );
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> GetImportStatus([FromQuery] ImportStatusFilterViewModel reqModel)
    {
        try
        {
            return await _dataImportService.GetImportStatus(reqModel);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.ImportStatus)
            );
        }
    }


    [HttpPost("[action]")]
    public async Task<ActionResult> ImportEmployeeTimeSheet([FromBody] EmployeeTimeSheetImportRequestDTO request)
    {
        try
        {
            return await _dataImportService.ImportEmployeeTimeSheet(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Importing, ResponseMessages.Timesheet)
            );
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult> ImportLeaveTakenOn([FromBody] LeaveTakenOnImportRequestDTO request)
    {
        try
        {
            JsonResult? res = await _dataImportService.LeaveTakenOnImport(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Importing,ResponseMessages.LeaveTakenOn));
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ImportLeaveTransactions([FromBody] LeaveTransactionImportRequestDTO request)
    {
        try
        {
            JsonResult? res = await _dataImportService.LeaveTransactionImport(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Importing, ResponseMessages.LeaveTransaction));
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult> UpsertESSUser([FromBody] UpsertESSUserRequestDTO request)
    {
        try
        {
            JsonResult? res = await _dataImportService.UpsertESSUser(request);
            return res;
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Updating, "ESS User", ex.Message));
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult> ImportEmployeeNumbers([FromBody] ImportEmployeeNumbersRequestDTO request)
    {
        try
        {
            JsonResult? res = await _dataImportService.ImportEmployeeNumbers(request);
            return res;
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Updating, "Employee Numbers", ex.Message));
        }
    }
}
