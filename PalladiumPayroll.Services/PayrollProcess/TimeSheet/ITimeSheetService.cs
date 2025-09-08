using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.TimeSheet;

namespace PalladiumPayroll.Services.PayrollProcess.TimeSheet
{
    public interface ITimeSheetService
    {
        Task<JsonResult> GetLatestImportedData(int companyId);
        Task<JsonResult> ImportTimeSheet(ImportTimeSheetRequest requestData);
        Task<JsonResult> ProcessTimeSheet();
    }
}
