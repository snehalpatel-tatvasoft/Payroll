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

     public async Task<JsonResult> GetPayrollCycleDropdown(long companyId)
    {
        List<PayrollCycleDropdownDTO> payrollCycle = await _singlePayslipRepository.GetPayrollCycleDropdown(companyId);
        
        return HttpStatusCodeResponse.SuccessResponse(payrollCycle, string.Format(ResponseMessages.Success, ResponseMessages.PayrollCycle, ActionType.Retrieved));
    }

     public async Task<JsonResult> GetNextUnprocessedPeriod(long companyId, long companyPayrollId)
    {
        string? processPeriod = await _singlePayslipRepository.GetNextUnprocessedPeriod(companyId,companyPayrollId);
        
        return HttpStatusCodeResponse.SuccessResponse(processPeriod, string.Format(ResponseMessages.Success, "Process Period", ActionType.Retrieved));
    }
}
