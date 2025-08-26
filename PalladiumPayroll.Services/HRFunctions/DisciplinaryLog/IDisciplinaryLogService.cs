using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.HRFunctions.DisciplinaryLog;

namespace PalladiumPayroll.Services.HRFunctions.DisciplinaryLog
{
    public interface IDisciplinaryLogService
    {
        Task<JsonResult> GetDisciplinaryLogByCompanyId(long companyId);
        Task<JsonResult> GetEmployeesForDisciplinaryLogDropdown(long companyId);
        Task<JsonResult> UpsertDisciplinaryLog(DisciplinaryLogUpsertDTO request);
        // Task<JsonResult> CreateDisciplinaryLog(DisciplinaryLogRequestDTO disciplinaryLog, IFormFile file);
        Task<JsonResult> GetDisciplinaryLogById(long disciplinaryLogId);
        // Task<JsonResult> UpdateDisciplinaryLog(DisciplinaryLogEditRequestDTO disciplinaryLog, IFormFile file);
        Task<JsonResult> DeleteDisciplinaryLog(long disciplinaryLogId);
    }
}