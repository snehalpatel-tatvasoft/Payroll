using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.PayrollProcess.SinglePayslip;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.PayrollProcess.SinglePayslip;

public class SinglePayslipService : ISinglePayslipService
{
    private readonly ISinglePayslipRepository _singlePayslipRepository;

    public SinglePayslipService(ISinglePayslipRepository singlePayslipRepository)
    {
        _singlePayslipRepository = singlePayslipRepository;
    }

    public async Task<JsonResult> GetEmployeesForProcessing(GetEmployeesForProcessingRequestDTO request)
    {
        var employees = await _singlePayslipRepository.GetEmployeesForProcessing(request);
        return HttpStatusCodeResponse.SuccessResponse(employees,
            string.Format(ResponseMessages.Success, "Employees for processing", ActionType.Retrieved));
    }


    public async Task<JsonResult> GetPayrollCycleDropdown(long companyId)
    {
        List<PayrollCycleDropdownDTO> payrollCycle = await _singlePayslipRepository.GetPayrollCycleDropdown(companyId);

        return HttpStatusCodeResponse.SuccessResponse(payrollCycle, string.Format(ResponseMessages.Success, ResponseMessages.PayrollCycle, ActionType.Retrieved));
    }

     public async Task<JsonResult> GetNextUnprocessedPeriods(long companyId, long companyPayrollId)
    {
        var processPeriod = await _singlePayslipRepository.GetNextUnprocessedPeriods(companyId,companyPayrollId);
        
        string? processPeriod = await _singlePayslipRepository.GetNextUnprocessedPeriod(companyId, companyPayrollId);

        return HttpStatusCodeResponse.SuccessResponse(processPeriod, string.Format(ResponseMessages.Success, "Process Period", ActionType.Retrieved));
    }
    public async Task<JsonResult> GetEmployeeRateAndDaysWorked(long employeeId, long processingPeriodId)
    {
        var result = await _singlePayslipRepository.GetEmployeeRateAndDaysWorked(employeeId, processingPeriodId);

        return HttpStatusCodeResponse.SuccessResponse(
            result,
            string.Format(ResponseMessages.Success, "Rate And Days Worked", ActionType.Retrieved)
        );
    }

}
