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
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@PayrollCycleId", request.PayrollCycleId);
        parameters.Add("@EmployeeStatusId", request.EmployeeStatusId);
        parameters.Add("@TransactionTypeId", request.TransactionTypeId);
        parameters.Add("@ProcessingCyclePeriodId", request.ProcessPeriodId);

        List<EmployeeForProcessingResponseDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeeForProcessingResponseDTO>(
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
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", employeeId);
        parameters.Add("@ProcessingPeriodId", processingPeriodId);

        EmployeeRateAndDaysWorkedDto? result = await _dapper.ExecuteStoredProcedureSingle<EmployeeRateAndDaysWorkedDto>(
            "usp_GetEmployeeRateAndDaysWorked",
            parameters
        );
        return result;
    }

    public async Task<List<TransactionListModelForPayslip>> GetModalTransactionsListForPayslip(int transactionId, int companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@TransactionId", transactionId);
        parameters.Add("@CompanyId", companyId);

        List<TransactionListModelForPayslip>? transactions = await _dapper.ExecuteStoredProcedure<TransactionListModelForPayslip>(
            "usp_GetTransactionsListForPayslip",
            parameters
        );
        return transactions ?? new List<TransactionListModelForPayslip>();
    }

    public async Task<long> ProcessSinglePayslip(ProcessSinglePayslipRequestDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@CompanyPayrollId", request.CompanyPayrollId);
        parameters.Add("@ProcessingCyclePeriodId", request.ProcessingCyclePeriodId);
        parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@PayslipType", request.PayslipType);
        parameters.Add("@StdTrans", request.StdTrans);
        parameters.Add("@TotalLeaveDays", request.TotalLeaveDays);
        parameters.Add("@TotalPresentDays", request.TotalPresentDays);

        DataTable? dt = new DataTable();
        dt.Columns.Add("TransactionID", typeof(int));
        dt.Columns.Add("TransactionType", typeof(string));
        dt.Columns.Add("TransactionName", typeof(string));
        dt.Columns.Add("TransactionValue", typeof(decimal));
        dt.Columns.Add("ETI", typeof(string));
        dt.Columns.Add("Recurring", typeof(bool));
        dt.Columns.Add("TransactionHours", typeof(decimal));

        foreach (PayslipDetailDTO? item in request.PayslipDetails)
        {
            dt.Rows.Add(
                item.PayrollProcessId,
                item.TransactionType ?? string.Empty,
                item.Description ?? string.Empty,
                item.Amount,
                item.ETI ?? string.Empty,
                item.IsRecurring,
                item.Hours
            );
        }

        parameters.Add("@PayslipDetails", dt.AsTableValuedParameter("dbo.NewPayrollDetails"));

        long result = await _dapper.ExecuteStoredProcedureSingle<long>(
            "usp_ProcessSinglePayslip", parameters);
        return result;
    }

    public async Task<List<SinglePayslipDetailsResponseDTO>> GetSinglePayslipDetails(GetSinglePayslipDetailsRequestDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@CompanyPayrollId", request.CompanyPayrollId);
        parameters.Add("@PayrollPeriodId", request.PayrollPeriodId);
        parameters.Add("@TransactionTypeId", request.TransactionTypeId);

        if (request.TransactionTypeId == 1)
        {
            parameters.Add("@RatePerHour", request.RatePerHour);
            parameters.Add("@NoOfDaysWorked", request.NoOfDaysWorked);

            List<SinglePayslipDetailsResponseDTO>? result = await _dapper.ExecuteStoredProcedure<SinglePayslipDetailsResponseDTO>(
                "usp_GetSinglePayslipDetails",
                parameters
            );
            return result ?? new List<SinglePayslipDetailsResponseDTO>();
        }
        else if (request.TransactionTypeId == 2)
        {
            parameters.Add("@RatePerHour", request.RatePerHour);
            parameters.Add("@NoOfDaysWorked", request.NoOfDaysWorked);

            DataTable? dt = new DataTable();
            dt.Columns.Add("TransactionID", typeof(long));
            dt.Columns.Add("TransactionType", typeof(string));
            dt.Columns.Add("TransactionName", typeof(string));
            dt.Columns.Add("TransactionValue", typeof(decimal));
            dt.Columns.Add("ETI", typeof(string));
            dt.Columns.Add("Recurring", typeof(bool));
            dt.Columns.Add("TransactionHours", typeof(decimal));

            foreach (TransactionDetailDTO? item in request.TransactionDetails)
            {
                dt.Rows.Add(item.TransactionID, item.TransactionType, item.TransactionName, item.TransactionValue, string.Empty, 0, 0);
            }
            parameters.Add("@TransactionDetails", dt.AsTableValuedParameter("dbo.NewPayrollDetails"));

            List<SinglePayslipDetailsResponseDTO>? result = await _dapper.ExecuteStoredProcedure<SinglePayslipDetailsResponseDTO>(
                "usp_DeductionGetSinglePayslipDetails",
                parameters
            );
            return result ?? new List<SinglePayslipDetailsResponseDTO>();
        }
        else if (request.TransactionTypeId == 3)
        {
            parameters.Add("@RatePerHour", request.RatePerHour);
            parameters.Add("@NoOfDaysWorked", request.NoOfDaysWorked);

            DataTable? dt = new DataTable();
            dt.Columns.Add("TransactionID", typeof(long));
            dt.Columns.Add("TransactionType", typeof(string));
            dt.Columns.Add("TransactionName", typeof(string));
            dt.Columns.Add("TransactionValue", typeof(decimal));
            dt.Columns.Add("ETI", typeof(string));
            dt.Columns.Add("Recurring", typeof(bool));
            dt.Columns.Add("TransactionHours", typeof(decimal));

            foreach (TransactionDetailDTO? item in request.TransactionDetails)
            {
                dt.Rows.Add(item.TransactionID, item.TransactionType, item.TransactionName, item.TransactionValue, string.Empty, 0, 0);
            }

            parameters.Add("@TransactionDetails", dt.AsTableValuedParameter("dbo.NewPayrollDetails"));

            List<SinglePayslipDetailsResponseDTO>? result = await _dapper.ExecuteStoredProcedure<SinglePayslipDetailsResponseDTO>(
                "usp_CompanyContributionGetSinglePayslipDetails",
                parameters
            );
            return result ?? new List<SinglePayslipDetailsResponseDTO>();
        }
        else if (request.TransactionTypeId == 4)
        {
            List<SinglePayslipDetailsResponseDTO>? result = await _dapper.ExecuteStoredProcedure<SinglePayslipDetailsResponseDTO>(
                "usp_FringeBenefitsGetSinglePayslipDetails",
                parameters
            );
            return result ?? new List<SinglePayslipDetailsResponseDTO>();
        }
        else
        {
            return new List<SinglePayslipDetailsResponseDTO>();
        }
    }

    public async Task<(decimal UIFCal, decimal UIFIncome)> GetUIFCalculation(GetSinglePayslipDetailsRequestDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@ProcessingCyclePeriodId", request.PayrollPeriodId);
        parameters.Add("@CycleType", "");

        DataTable dt = new DataTable();
        dt.Columns.Add("TransactionID", typeof(long));
        dt.Columns.Add("TransactionType", typeof(string));
        dt.Columns.Add("TransactionName", typeof(string));
        dt.Columns.Add("TransactionValue", typeof(decimal));
        dt.Columns.Add("ETI", typeof(string));
        dt.Columns.Add("Recurring", typeof(bool));
        dt.Columns.Add("TransactionHours", typeof(decimal));

        foreach (TransactionDetailDTO? item in request.TransactionDetails)
        {
            dt.Rows.Add(item.TransactionID, item.TransactionType ?? "", item.TransactionName ?? "", item.TransactionValue, string.Empty, 0, 0);
        }

        parameters.Add("@TransactionDetails", dt.AsTableValuedParameter("dbo.NewPayrollDetails"));
        parameters.Add("@UIFCal", dbType: DbType.Decimal, direction: ParameterDirection.Output);
        parameters.Add("@UIFIncome", dbType: DbType.Decimal, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<(decimal UIFCal, decimal UIFIncome)>("usp_UIFCalculation_Normal", parameters);

        decimal uifCal = parameters.Get<decimal>("@UIFCal");
        decimal uifIncome = parameters.Get<decimal>("@UIFIncome");

        return (uifCal, uifIncome);
    }

    public async Task<bool> DeleteSinglePayslipTransactions(List<PayslipDeleteTransactionDTO> transactions)
    {
        DynamicParameters? parameters = new DynamicParameters();
        DataTable dt = new DataTable();
        dt.Columns.Add("EmployeePayslipPreviewDtlId", typeof(int));

        foreach (PayslipDeleteTransactionDTO? item in transactions)
        {
            dt.Rows.Add(item.EmployeePayslipPreviewDtlId);
        }
        parameters.Add("@Transactions", dt.AsTableValuedParameter("dbo.PayslipDeleteTransactionType"));

        await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteSinglePayslipTransactions", parameters);
        return true;
    }

    public async Task<List<EmployeeLeaveDetailResponseDTO>> GetEmployeeLeaveDetails(long employeeId, long processingCyclePeriodId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", employeeId);
        parameters.Add("@ProcessingCyclePeriodId", processingCyclePeriodId);

        List<EmployeeLeaveDetailResponseDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeeLeaveDetailResponseDTO>(
            "usp_GetEmployeeLeaveDetailsForPayslip",
            parameters
        );
        return result?.ToList() ?? new List<EmployeeLeaveDetailResponseDTO>();
    }

    public async Task<List<EmployeeLeaveDetailResponseDTO>> GetEmployeeLeaveHistory(long employeeId, long processingCyclePeriodId, long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", employeeId);
        parameters.Add("@ProcessingCyclePeriodId", processingCyclePeriodId);
        parameters.Add("@CompanyId", companyId);

        List<EmployeeLeaveDetailResponseDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeeLeaveDetailResponseDTO>(
            "usp_GetEmployeeLeaveHistoryForPayslip",
            parameters
        );
        return result?.ToList() ?? new List<EmployeeLeaveDetailResponseDTO>();
    }

    public async Task<PayslipPreviewHeaderDTO?> GetPayslipPreviewDetails(int payslipPreviewId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@PayslipPreviewId", payslipPreviewId);

        return await _dapper.ExecuteStoredProcedureMultipleAsync(
            "LoadPayslipPreviewDetails",
            parameters,
            async multi =>
            {
                PayslipPreviewHeaderDTO? header = await multi.ReadFirstOrDefaultAsync<PayslipPreviewHeaderDTO>();
                List<PayslipPreviewDetailDTO>? details = (await multi.ReadAsync<PayslipPreviewDetailDTO>()).ToList();

                if (header != null)
                    header.Details = details;

                return header;
            });
    }

    public async Task<long> SaveSinglePayslip(long employeePayslipPreviewId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeePayslipPreviewId", employeePayslipPreviewId);
        parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

        long result = await _dapper.ExecuteStoredProcedureSingle<long>(
            "usp_SaveSinglePayslip", parameters);
        return result;
    }


    public async Task<CalculateLeavePayoutResultDTO?> CalculateLeavePayout(int empId, int companyPayrollId, int periodId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmpId", empId);
        parameters.Add("@CompanyPayrollId", companyPayrollId);
        parameters.Add("@PeriodId", periodId);

        CalculateLeavePayoutResultDTO? resultList = await _dapper.ExecuteStoredProcedureSingle<CalculateLeavePayoutResultDTO>(
            "usp_CalculateLeavePayout",
            parameters
        );
        return resultList;
    }

    public async Task<ManageEmploymentStatusResult?> ManageEndEmployment(ManageEndEmploymentDTO dto)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyPayrollId", dto.CompanyPayrollId);
        parameters.Add("@EmpId", dto.EmpId);
        parameters.Add("@EndEmpmntDate", dto.EndEmploymentDate);
        parameters.Add("@PeriodId", dto.PeriodId);
        parameters.Add("@EmpStatus", dto.EmpStatus);
        parameters.Add("@LeavePaidOutAmt", dto.LeavePaidOutAmt);

        ManageEmploymentStatusResult? result = await _dapper.ExecuteStoredProcedureSingle<ManageEmploymentStatusResult>(
            "usp_ManageEndEmployment",
            parameters
        );

        return result ?? new ManageEmploymentStatusResult
        {
            Result = -1,
            ErrorMessage = "No response from stored procedure."
        };
    }

    public async Task<ManageEmploymentStatusResult?> ReinstateEmployee(ReinstateEmployeeDTO dto)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyPayrollId", dto.CompanyPayrollId);
        parameters.Add("@EmpId", dto.EmpId);
        parameters.Add("@PeriodId", dto.PeriodId);
        parameters.Add("@ReinstateType", dto.ReinstateType);

        ManageEmploymentStatusResult? result = await _dapper.ExecuteStoredProcedureSingle<ManageEmploymentStatusResult>(
            "usp_ManageReinstateEmployee",
            parameters
        );

        return result ?? new ManageEmploymentStatusResult
        {
            Result = -1,
            ErrorMessage = "No response from stored procedure."
        };
    }

    public async Task<int?> CheckEndEmploymentStatus(int employeeId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmpId", employeeId);

        int? result = await _dapper.ExecuteStoredProcedureSingle<int>(
            "usp_CheckEndEmploymentStatus",
            parameters
        );
        return result;
    }

    public async Task<UndoRedoPayslipResult> UndoEmployeeSinglePayslip(UndoPayslipRequestDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

        UndoRedoPayslipResult? result = await _dapper.ExecuteStoredProcedureSingle<UndoRedoPayslipResult>(
            "usp_UndoEmployeeSinglePayslip", parameters
        );

        return result ?? new UndoRedoPayslipResult
        {
            Result = "Failure",
            Message = "No response from stored procedure."
        };
    }

    public async Task<UndoRedoAvailabilityResult?> CheckUndoRedoAvailability(long employeeId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", employeeId);

        UndoRedoAvailabilityResult? result = await _dapper.ExecuteStoredProcedureSingle<UndoRedoAvailabilityResult>(
            "usp_CheckUndoRedoAvailability", parameters
        );

        return result;
    }

    public async Task<UndoRedoPayslipResult> RedoEmployeeSinglePayslip(RedoPayslipRequestDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@RedoPayslipId", request.RedoPayslipId);
        parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

        UndoRedoPayslipResult? result = await _dapper.ExecuteStoredProcedureSingle<UndoRedoPayslipResult>(
            "usp_RedoEmployeeSinglePayslip", parameters
        );

        return result ?? new UndoRedoPayslipResult
        {
            Result = "Failure",
            Message = "No response from stored procedure."
        };
    }


}

