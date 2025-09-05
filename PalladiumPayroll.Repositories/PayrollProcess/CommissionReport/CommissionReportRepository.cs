using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.PayrollProcess;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.PayrollProcess;
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

    public async Task<JsonResult> GetPayPeriods(int cycleId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CycleId", cycleId);
        var result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_GetPayPeriods", parameters);
        var mappedResult = result.Select(p => new
        {
            processingCyclePeriodId = p.Id,
            processPeriod = p.Value
        }).ToList();
        return HttpStatusCodeResponse.SuccessResponse(mappedResult, string.Format(ResponseMessages.Success, ResponseMessages.CommissionReport + " pay periods", ActionType.Retrieved));
    }

    public async Task<string?> ImportCommissions(ImportCommissionRequestDTO request)
    {
        var table = CommissionsToDataTable(request.Commissions);

        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@CommissionType", request.CommissionType);
        parameters.Add("@Commissions", table.AsTableValuedParameter("dbo.CommissionImportType"));
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

        var result = await _dapper.ExecuteStoredProcedureSingle<string>("usp_ImportCommissions", parameters);
        return result == "SUCCESS" ? null : result;
    }

    private DataTable CommissionsToDataTable(List<CommissionReportRequestDTO> commissions)
    {
        var table = new DataTable();
        table.Columns.Add("EmployeeCode", typeof(string));
        table.Columns.Add("Commission", typeof(decimal));

        foreach (var item in commissions)
        {
            table.Rows.Add(
                item.EmployeeCode,
                item.Commission
            );
        }

        return table;
    }
    public async Task<JsonResult> GetCommissions(int companyId, int? cycleId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@CycleId", cycleId, dbType: DbType.Int32, direction: ParameterDirection.Input);

        var result = await _dapper.ExecuteStoredProcedure<CommissionReportResponseDTO>("usp_GetCommissions", parameters);
        var mappedResult = result.Select(p => new
        {
            id = p.Id,
            employeeCode = p.EmployeeCode,
            employeeName = p.EmployeeName,
            commission = p.Commission
        }).ToList();

        return HttpStatusCodeResponse.SuccessResponse(mappedResult, string.Format(ResponseMessages.Success, ResponseMessages.CommissionReport + " data", ActionType.Retrieved));
    }
    public async Task<string?> ProcessCommission(ProcessCommissionRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@CycleId", request.CycleId, dbType: DbType.Int32, direction: ParameterDirection.Input);
        parameters.Add("@PeriodId", request.PeriodId, dbType: DbType.Int32, direction: ParameterDirection.Input);

        // Convert commissionIds to DataTable for table-valued parameter
        var commissionIdsTable = new DataTable();
        commissionIdsTable.Columns.Add("Id", typeof(long));
        foreach (var id in request.CommissionIds)
        {
            commissionIdsTable.Rows.Add(id);
        }
        parameters.Add("@CommissionIds", commissionIdsTable.AsTableValuedParameter("dbo.IdListType"));

        var result = await _dapper.ExecuteStoredProcedureSingle<string>("usp_ProcessCommission", parameters);
        return result == "SUCCESS" ? null : result;
    }
}