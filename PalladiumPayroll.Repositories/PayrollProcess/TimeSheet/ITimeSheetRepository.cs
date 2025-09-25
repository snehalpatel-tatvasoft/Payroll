using PalladiumPayroll.DTOs.DTOs.PayrollProcess.TimeSheet;
using System.Data;

namespace PalladiumPayroll.Repositories.PayrollProcess.TimeSheet
{
    public interface ITimeSheetRepository
    {
        Task<List<TimeSheetImport>> GetLatestImportedData(int companyId);
        Task<bool> ImportTimeSheetData(DataTable timeSheetDataTable);
        Task<bool> ProcessTimeSheet();
    }
}
