using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.Miscellaneous;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Repositories.PayrollProcess.CommissionReport;

public class CommissionReportRepository : ICommissionReportRepository
{
    private readonly DapperContext _dapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommissionReportRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _dapper = new DapperContext(configuration);
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<JsonResult> GetPayrollCycles(int companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        var result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_GetPayrollCycles", parameters);
        var mappedResult = result.Select(p => new
        {
            companyPayrollId = p.Id,
            cycleName = p.Value
        }).ToList();
        return HttpStatusCodeResponse.SuccessResponse(mappedResult, string.Format(ResponseMessages.Success, ResponseMessages.CommissionReport + " payroll cycles", ActionType.Retrieved));
    }

    public async Task<JsonResult> GetPayPeriods(int companyId, int cycleId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@CycleId", cycleId);
        var result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_GetPayPeriods", parameters);
        var mappedResult = result.Select(p => new
        {
            processingCyclePeriodId = p.Id,
            processPeriod = p.Value
        }).ToList();
        return HttpStatusCodeResponse.SuccessResponse(mappedResult, string.Format(ResponseMessages.Success, ResponseMessages.CommissionReport + " pay periods", ActionType.Retrieved));
    }
}