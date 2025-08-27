using PalladiumPayroll.DTOs.DTOs.RequestDTOs.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.HRFunctions.DisciplinaryLog;

namespace PalladiumPayroll.Repositories.HRFunctions.DisciplinaryLog
{
    public interface IDisciplinaryLogRepository
    {
        Task<List<DisciplinaryLogResponseDTO>> GetDisciplinaryLogByCompanyId(long companyId);
        Task<List<EmployeeDropdownDTO>> GetEmployeesForDisciplinaryLogDropdown(long companyId);
        Task<bool> UpsertDisciplinaryLog(DisciplinaryLogUpsertDTO request);
        Task<DisciplinaryLogByIdResponseDTO> GetDisciplinaryLogById(long disciplinaryLogId);
        Task<int> DeleteDisciplinaryLog(long disciplinaryLogId);
    }
}