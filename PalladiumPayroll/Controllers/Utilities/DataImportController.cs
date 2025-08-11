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
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Transaction));
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
                string.Format(ResponseMessages.Exception, ActionType.Saving, "Import year to date template.")
            );
        }
    }
}
