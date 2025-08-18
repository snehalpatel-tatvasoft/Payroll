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
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
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
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
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
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
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
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ImportYTDRecord([FromBody] List<YearToDateRecordDTO> request)
    {
        try
        {
            return await _dataImportService.ImportYTDRecord(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
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
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Saving,"Employee Masterfile Import", ex.Message));
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> GetImportStatus([FromQuery] ImportStatusFilterViewModel reqModel)
    {
        try
        {
            return await _dataImportService.GetImportStatus(reqModel);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ex.Message);
        }
    }
}
