using System.Net;
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Transaction, ex.Message));
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
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Transaction, ex.Message));
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
}
