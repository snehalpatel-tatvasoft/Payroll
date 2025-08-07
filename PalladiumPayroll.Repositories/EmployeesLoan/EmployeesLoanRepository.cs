using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.EmployeesLoan;
using System.Data;

namespace PalladiumPayroll.Repositories.EmployeesLoan;

public class EmployeesLoanRepository : IEmployeesLoanRepository
{
    private readonly DapperContext _dapper;
    private readonly IConfiguration _configuration;

    public EmployeesLoanRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dapper = new DapperContext(_configuration);
    }

    public async Task<bool> CreateEmployeeLoan(EmployeeLoanRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@RepaymentStartDate", request.RepaymentStartDate);
        parameters.Add("@LoanGrantedDate", request.LoanGrantedDate);
        parameters.Add("@NumberOfRepayment", request.NumberOfRepayment);
        parameters.Add("@CurrentRepayment", request.CurrentrePaymentAmount);
        parameters.Add("@LoanAmount", request.LoanAmount);
        parameters.Add("@LoanMaxdeductionInterestRate", request.LoanMaxdeductionInterestRate);
        parameters.Add("@InterestRate", request.InterestRate);
        parameters.Add("@ActualLoanAmount", request.ActualLoanAmount);
        parameters.Add("@LoanIntegration", request.LoanIntegration);
        parameters.Add("@CreatedBy", request.UserId);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CreateEmployeeLoan", parameters);
    }

    public async Task<bool> UpdateEmployeeLoan(EmployeeLoanRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@LoanId", request.EmployeeLoanId);
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@RepaymentStartDate", request.RepaymentStartDate);
        parameters.Add("@LoanGrantedDate", request.LoanGrantedDate);
        parameters.Add("@NumberOfRepayment", request.NumberOfRepayment);
        parameters.Add("@CurrentRepayment", request.CurrentrePaymentAmount);
        parameters.Add("@LoanAmount", request.LoanAmount);
        parameters.Add("@LoanMaxdeductionInterestRate", request.LoanMaxdeductionInterestRate);
        parameters.Add("@InterestRate", request.InterestRate);
        parameters.Add("@ActualLoanAmount", request.ActualLoanAmount);
        parameters.Add("@LoanIntegration", request.LoanIntegration);
        parameters.Add("@LastUpdatedBy", request.UserId);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdateEmployeeLoan", parameters);
    }

    public async Task<bool> PauseEmployeeLoan(long employeeLoanId, long updatedBy)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@LoanId", employeeLoanId);
        parameters.Add("@UpdatedBy", updatedBy);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_PauseEmployeeLoan", parameters);
    }

    public async Task<bool> FullPaidEmployeeLoan(long employeeLoanId, long updatedBy)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@LoanId", employeeLoanId);
        parameters.Add("@UpdatedBy", updatedBy);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_FullPaidEmployeeLoan", parameters);
    }
    public async Task<TableDataModel<EmployeeLoanResponseDTO>> GetLoansByCompanyId(LoanFilterViewModel reqModel)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", reqModel.CompanyId);
        parameters.Add("@LoanStatus", reqModel.LoanStatus);
        parameters.Add("@CurrentPage", reqModel.CurrentPage);
        parameters.Add("@PageSize", reqModel.PageSize);
        parameters.Add("@SortBy", reqModel.SortBy ?? "LoanGrantedDate");
        parameters.Add("@SortType", reqModel.sortType == true ? "ASC" : "DESC");
        parameters.Add("@Search", reqModel.Search ?? "");
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var data = await _dapper.ExecuteStoredProcedure<EmployeeLoanResponseDTO>("usp_GetLoanDetailsByCompanyId", parameters);
        var total = parameters.Get<int>("@TotalCount");

        return new TableDataModel<EmployeeLoanResponseDTO>
        {
            DataList = data,
            TotalCount = total
        };
    }


    public async Task<EmployeeLoanDropdownsDTO> GetEmployeeLoanDropdowns(long companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        return await _dapper.ExecuteStoredProcedureMultipleAsync(
            "usp_GetDropdownDataForEmployeeLoan",
            parameters,
            async multi =>
            {
                var dto = new EmployeeLoanDropdownsDTO
                {
                    Employees = (await multi.ReadAsync<EmployeeLoanDataDropdownDto>()).ToList()
                };
                return dto;
            });
    }


}
