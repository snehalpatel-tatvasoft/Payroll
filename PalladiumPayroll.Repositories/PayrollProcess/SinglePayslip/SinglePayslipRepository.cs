using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;
namespace PalladiumPayroll.Repositories.PayrollProcess.SinglePayslip;

public class SinglePayslipRepository : ISinglePayslipRepository
{
    private readonly DapperContext _dapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SinglePayslipRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _dapper = new DapperContext(configuration);
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<EmployeeForProcessingResponseDTO>> GetEmployeesForProcessing(GetEmployeesForProcessingRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@PayrollCycleId", request.PayrollCycleId);
        parameters.Add("@EmployeeStatusId", request.EmployeeStatusId);
        parameters.Add("@TransactionTypeId", request.TransactionTypeId);
        parameters.Add("@ProcessPeriod", request.ProcessPeriod);

        var result = await _dapper.ExecuteStoredProcedure<EmployeeForProcessingResponseDTO>(
            "usp_GetEmployeesForProcessing", parameters);
        return result.ToList();
    }

    public async Task<List<PayrollCycleDropdownDTO>> GetPayrollCycleDropdown(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<PayrollCycleDropdownDTO>? result = await _dapper.ExecuteStoredProcedure<PayrollCycleDropdownDTO>(
            "usp_GetSinglePayslipPayrollCycleDropdown",
            parameters
        );

        return result ?? new List<PayrollCycleDropdownDTO>();
    }

    public async Task<string?> GetNextUnprocessedPeriod(long companyId, long companyPayrollId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@CompanyPayrollId", companyPayrollId);

        string? result = await _dapper.ExecuteStoredProcedureSingle<string>(
            "usp_GetNextProcessingPeriod",
            parameters
        );

        return result; 
    }
}
