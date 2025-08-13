using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

namespace PalladiumPayroll.Repositories.Utilities.DataImport;

public interface IDataImportRepository
{
    Task<TableDataModel<PayrollProcessingTransactionDto>> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel);
    Task<string?> EmployeeMasterfileImport(EmployeeMasterImportRequestDTO request);

    Task<bool> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request);

    Task<List<YTDTemplateDropdownDto>> GetDropDownForYearToDateTemplate(long companyId);

    Task<List<TransactionForExcelGenerateDto>> GetTransactionForExcelGenerate(int templateId);

    Task<bool> ImportYTDRecord(YearToDateRecordDTO record, int createdBy);

    Task<bool> ImportWorkInformation(WorkInformationDTO record, string userId);
}
