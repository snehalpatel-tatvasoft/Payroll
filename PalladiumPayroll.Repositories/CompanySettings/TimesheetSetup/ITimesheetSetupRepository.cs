using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Repositories.CompanySettings
{
    public interface ITimesheetSetupRepository
    {
        Task<List<TimesheetSetupResponseDTO>> GetPayrollCycles(long companyId);
        Task<List<TimesheetPayrollSetupResponseDTO>> GetTimesheetPayrollSetup(long companyId);
        Task<bool> UpsertTimesheetPayrollSetup(TimesheetSetupRequestDTO request);
    }
}