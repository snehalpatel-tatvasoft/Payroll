using System.Data;
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
        parameters.Add("@ProcessingCyclePeriodId", request.ProcessPeriodId);

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

    public async Task<List<ProcessingPeriodDTO>> GetNextUnprocessedPeriods(long companyId, long companyPayrollId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@CompanyPayrollId", companyPayrollId);

        List<ProcessingPeriodDTO>? result = await _dapper.ExecuteStoredProcedure<ProcessingPeriodDTO>(
            "usp_GetNextProcessingPeriod",
            parameters
        );

        return result ?? new List<ProcessingPeriodDTO>();
    }

    public async Task<EmployeeRateAndDaysWorkedDto?> GetEmployeeRateAndDaysWorked(long employeeId, long processingPeriodId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", employeeId);
        parameters.Add("@ProcessingPeriodId", processingPeriodId);

        var result = await _dapper.ExecuteStoredProcedureSingle<EmployeeRateAndDaysWorkedDto>(
            "usp_GetEmployeeRateAndDaysWorked",
            parameters
        );

        return result;
    }
    public async Task<List<TransactionListModelForPayslip>> GetModalTransactionsListForPayslip(int transactionId, int companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TransactionId", transactionId);
        parameters.Add("@CompanyId", companyId);


        var transactions = await _dapper.ExecuteStoredProcedure<TransactionListModelForPayslip>(
            "usp_GetTransactionsListForPayslip",
            parameters
        );

        return transactions ?? new List<TransactionListModelForPayslip>();
    }
    public async Task<long> ProcessSinglePayslip(ProcessSinglePayslipRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@CompanyPayrollId", request.CompanyPayrollId);
        parameters.Add("@ProcessingCyclePeriodId", request.ProcessingCyclePeriodId);
        parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

        // Create DataTable for TVP
        var dt = new DataTable();
        dt.Columns.Add("PayrollCycleId", typeof(long));
        dt.Columns.Add("Description", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("IsRecurring", typeof(bool));
        dt.Columns.Add("Hours", typeof(decimal));

        foreach (var item in request.PayslipDetails)
        {
            dt.Rows.Add(item.PayrollProcessId, item.Description, item.Amount, item.IsRecurring, item.Hours);
        }

        // Add TVP parameter
        parameters.Add("@PayslipDetails", dt.AsTableValuedParameter("dbo.PayslipDetailType"));

        // Call the new SP
        var result = await _dapper.ExecuteStoredProcedureSingle<long>(
            "usp_ProcessSinglePayslip", parameters);

        return result;
    }
    public async Task<List<SinglePayslipDetailsResponseDTO?>> GetSinglePayslipDetails(GetSinglePayslipDetailsRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@CompanyPayrollId", request.CompanyPayrollId);
        parameters.Add("@PayrollPeriodId", request.PayrollPeriodId);
        parameters.Add("@TransactionTypeId", request.TransactionTypeId);
        parameters.Add("@NoOfDaysWorked", request.NoOfDaysWorked);
        parameters.Add("@RatePerHour", request.RatePerHour);

        var result = await _dapper.ExecuteStoredProcedure<SinglePayslipDetailsResponseDTO>(
            "usp_GetSinglePayslipDetails",
            parameters
        );

        return result ?? new List<SinglePayslipDetailsResponseDTO>() ;
    }


}
