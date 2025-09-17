using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;
using System.Collections.Generic;
using System.Threading.Tasks;

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
}