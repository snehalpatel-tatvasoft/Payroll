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
}