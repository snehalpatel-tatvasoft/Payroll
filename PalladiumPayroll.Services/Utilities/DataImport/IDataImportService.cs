using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

namespace PalladiumPayroll.Services.Utilities.DataImport;

public interface IDataImportService
{
    Task<JsonResult> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel);
    Task<JsonResult> EmployeeMasterfileImport(EmployeeMasterImportRequestDTO request);
    Task<JsonResult> UpsertESSUser(UpsertESSUserRequestDTO request);
    Task<JsonResult> ImportEmployeeNumbers(ImportEmployeeNumbersRequestDTO request);

    Task<JsonResult> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request);

    Task<JsonResult> GetDropDownForYearToDateTemplate(long companyId);

    Task<JsonResult> GetTransactionForExcelGenerate(int templateId);

    Task<JsonResult> ImportYTDRecord(ImportYearToDateRecordRequestDTO importDto);

     Task<JsonResult> ImportWorkInformation(WorkInformationImportRequestDTO request);

     Task<JsonResult> GetImportStatus(ImportStatusFilterViewModel reqModel);
}


