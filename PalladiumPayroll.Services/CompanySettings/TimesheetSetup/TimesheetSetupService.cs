using Microsoft.AspNetCore.Mvc;
using System.Net;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;

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
            try
            {
                var cycles = await _timesheetSetupRepository.GetPayrollCycles(companyId);
                if (cycles.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(cycles, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Payroll Cycles");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching payroll cycles: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetTimesheetPayrollSetup(long companyId)
        {
            try
            {
                var setup = await _timesheetSetupRepository.GetTimesheetPayrollSetup(companyId);
                return HttpStatusCodeResponse.SuccessResponse(setup, ResponseMessages.DataFetchSuccess);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching timesheet setup: {ex.Message}");
            }
        }

        public async Task<JsonResult> UpsertTimesheetPayrollSetup(TimesheetSetupRequestDTO request)
        {
            try
            {
                if (request.CompanyId <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid CompanyId.");
                }

                var setup = await _timesheetSetupRepository.UpsertTimesheetPayrollSetup(request);
                return HttpStatusCodeResponse.SuccessResponse(setup, "Timesheet setup saved successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Failed to save timesheet setup: {ex.Message}");
            }
        }
    }
}