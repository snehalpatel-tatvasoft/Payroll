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

    public async Task<JsonResult> EmployeeMasterfileImport(EmployeeMasterImportRequestDTO request)
    {
        var errorMessage = await _dataImportRepository.EmployeeMasterfileImport(request);

        if (!string.IsNullOrEmpty(errorMessage))
            return HttpStatusCodeResponse.InternalServerErrorResponse(errorMessage);

       return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeMasterfile, ActionType.Imported));
    }

    public async Task<JsonResult> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request)
    {
        bool isAdded = await _dataImportRepository.AddImportYearToDateTemplate(request);

        if (isAdded)
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.YearToDateTemplate, ActionType.Saved));

        return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.AlreadyExist, "Template with this name"));
    }

    public async Task<JsonResult> GetDropDownForYearToDateTemplate(long companyId)
    {
        List<YTDTemplateDropdownDto> template = await _dataImportRepository.GetDropDownForYearToDateTemplate(companyId);

        return HttpStatusCodeResponse.SuccessResponse(template, string.Format(ResponseMessages.Success, ResponseMessages.YearToDateTemplate, ActionType.Retrieved));
    }

    public async Task<JsonResult> GetTransactionForExcelGenerate(int templateId)
    {
        List<TransactionForExcelGenerateDto> transactions = await _dataImportRepository.GetTransactionForExcelGenerate(templateId);

        return HttpStatusCodeResponse.SuccessResponse(transactions, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Retrieved));
    }

    public async Task<JsonResult> ImportYTDRecord(ImportYearToDateRecordRequestDTO importDto)
    {
        string resultMessage = await _dataImportRepository.ImportYTDRecords(importDto);
        if (resultMessage == "SUCCESS")
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.YearToDateTemplate, ActionType.Imported));
        }
        else
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("Failed to Import File.");
        }
    }

    public async Task<JsonResult> ImportWorkInformation(WorkInformationImportRequestDTO request)
    {
        string resultMessage = await _dataImportRepository.ImportWorkInformation(request);

        if (resultMessage == "SUCCESS")
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.YearToDateTemplate, ActionType.Imported));
        }
        else
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("Failed to Import File");
        }
    }

    public async Task<JsonResult> GetImportStatus(ImportStatusFilterViewModel reqModel)
    {
        TableDataModel<ImportStatusDto> status = await _dataImportRepository.GetImportStatus(reqModel);

        return HttpStatusCodeResponse.SuccessResponse(status, string.Format(ResponseMessages.Success, ResponseMessages.ImportStatus, ActionType.Retrieved));
    }

    public async Task<JsonResult> ImportEmployeeTimeSheet(EmployeeTimeSheetImportRequestDTO request)
    {
        string resultMessage = await _dataImportRepository.ImportEmployeeTimeSheet(request);

        if (resultMessage == "SUCCESS")
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Timesheet, ActionType.Imported));
        }
        else
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("Failed to Import File");
        }
    }

}
