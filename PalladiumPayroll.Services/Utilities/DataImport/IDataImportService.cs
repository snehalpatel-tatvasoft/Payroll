using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

namespace PalladiumPayroll.Services.Utilities.DataImport;

public interface IDataImportService
{
    Task<JsonResult> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel);

    Task<JsonResult> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request);

    Task<JsonResult> GetDropDownForYearToDateTemplate(long companyId);

    Task<JsonResult> GetTransactionForExcelGenerate(int templateId);
}
