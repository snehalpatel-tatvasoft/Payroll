using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.CompanySettings
{
    public class TimesheetSetupService : ITimesheetSetupService
    {
        private readonly ITimesheetSetupRepository _timesheetSetupRepository;

        public TimesheetSetupService(ITimesheetSetupRepository timesheetSetupRepository)
        {
            _timesheetSetupRepository = timesheetSetupRepository;
        }

        public async Task<JsonResult> GetPayrollCycles(long companyId)
        {
            List<TimesheetSetupResponseDTO>? cycles = await _timesheetSetupRepository.GetPayrollCycles(companyId);

            return HttpStatusCodeResponse.SuccessResponse(cycles, string.Format(ResponseMessages.Success, ResponseMessages.PayrollCycle, ActionType.Retrieved));
        }

        public async Task<JsonResult> GetTimesheetPayrollSetup(long companyId)
        {
            List<TimesheetPayrollSetupResponseDTO>? setup = await _timesheetSetupRepository.GetTimesheetPayrollSetup(companyId);

            return HttpStatusCodeResponse.SuccessResponse(setup, string.Format(ResponseMessages.Success, ResponseMessages.TimesheetSetup, ActionType.Retrieved));
        }

        public async Task<JsonResult> UpsertTimesheetPayrollSetup(TimesheetSetupRequestDTO request)
        {
            bool isSaved = await _timesheetSetupRepository.UpsertTimesheetPayrollSetup(request);

            return isSaved
            ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.TimesheetSetup, ActionType.Saved))
            : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableSaveTimesheetSetup);
        }
    }
}