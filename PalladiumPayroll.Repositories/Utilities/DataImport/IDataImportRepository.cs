using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

namespace PalladiumPayroll.Repositories.Utilities.DataImport;

public interface IDataImportRepository
{
    Task<TableDataModel<PayrollProcessingTransactionDto>> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel);
    Task<string?> EmployeeMasterfileImport(EmployeeMasterImportRequestDTO request);
    Task<string?> UpsertESSUser(UpsertESSUserRequestDTO request);
    Task<string?> ImportEmployeeNumbers(ImportEmployeeNumbersRequestDTO request);
    Task<bool> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request);
    Task<List<YTDTemplateDropdownDto>> GetDropDownForYearToDateTemplate(long companyId);
    Task<List<TransactionForExcelGenerateDto>> GetTransactionForExcelGenerate(int templateId);
    Task<string> ImportYTDRecords(ImportYearToDateRecordRequestDTO importDto);
    Task<string> ImportWorkInformation(WorkInformationImportRequestDTO importDto);
    Task<TableDataModel<ImportStatusDto>> GetImportStatus(ImportStatusFilterViewModel reqModel);
}
