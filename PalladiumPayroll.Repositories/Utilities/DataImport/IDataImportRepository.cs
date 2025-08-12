using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

namespace PalladiumPayroll.Repositories.Utilities.DataImport;

public interface IDataImportRepository
{
    Task<TableDataModel<PayrollProcessingTransactionDto>> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel);

    Task<bool> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request);

    Task<List<YTDTemplateDropdownDto>> GetDropDownForYearToDateTemplate(long companyId);
}
