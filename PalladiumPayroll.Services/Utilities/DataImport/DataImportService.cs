using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Utilities.DataImport;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Utilities.DataImport;

public class DataImportService : IDataImportService
{
    private readonly IDataImportRepository _dataImportRepository;

    public DataImportService(IDataImportRepository dataImportRepository)
    {
        _dataImportRepository = dataImportRepository;
    }

    public async Task<JsonResult> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel)
    {
        TableDataModel<PayrollProcessingTransactionDto> transactions = await _dataImportRepository.GetPayrollProcessingTransactionsByCompany(reqModel);

        return HttpStatusCodeResponse.SuccessResponse(transactions, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Retrieved));
    }

    public async Task<JsonResult> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request)
    {
        bool isAdded = await _dataImportRepository.AddImportYearToDateTemplate(request);

        if (isAdded)
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Import Year to Date Template ", ActionType.Saved));

        return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.AlreadyExist, "Template with this name"));
    }

    public async Task<JsonResult> GetDropDownForYearToDateTemplate(long companyId)
    {
        List<YTDTemplateDropdownDto> template = await _dataImportRepository.GetDropDownForYearToDateTemplate(companyId);

        return HttpStatusCodeResponse.SuccessResponse(template, string.Format(ResponseMessages.Success, "Year to date Template", ActionType.Retrieved));
    }

}
