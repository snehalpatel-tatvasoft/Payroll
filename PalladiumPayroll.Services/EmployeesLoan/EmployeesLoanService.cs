using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.EmployeesLoan;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.EmployeesLoan;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.EmployeesLoan;

public class EmployeesLoanService : IEmployeesLoanService
{
    private readonly IEmployeesLoanRepository _repository;

    public EmployeesLoanService(IEmployeesLoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<JsonResult> CreateEmployeeLoan(EmployeeLoanRequestDTO request)
    {
        try
        {
            var result = await _repository.CreateEmployeeLoan(request);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.LoanCreatedSuccessfully);
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.LoanCreationFailed);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ex.Message);
        }
    }
    public async Task<JsonResult> UpdateEmployeeLoan(EmployeeLoanRequestDTO request)
    {
        try
        {
            var result = await _repository.UpdateEmployeeLoan(request);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.LoanUpdatedSuccessfully);
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.LoanUpdatedFailed);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ex.Message);
        }
    }
    public async Task<JsonResult> PauseEmployeeLoan(long employeeLoanId, long updatedBy)
    {
        try
        {
            var result = await _repository.PauseEmployeeLoan(employeeLoanId, updatedBy);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.LoanPausedSuccessfully);
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.LoanPausedFailed);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ex.Message);
        }
    }
    public async Task<JsonResult> FullPaidEmployeeLoan(long employeeLoanId, long updatedBy)
    {
        try
        {
            var result = await _repository.FullPaidEmployeeLoan(employeeLoanId, updatedBy);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.LoanPaidSuccessfully);
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.LoanPaidFailed);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ex.Message);
        }
    }
    public async Task<JsonResult> GetLoansByCompanyId(LoanFilterViewModel reqModel)
    {
        try
        {
            var result = await _repository.GetLoansByCompanyId(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(result, ResponseMessages.LoanDataFetchedSuccessfully);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ex.Message);
        }
    }

    public async Task<EmployeeLoanDropdownsDTO> GetEmployeeLoanDropdowns(long companyId)
    {
        try
        {
            return await _repository.GetEmployeeLoanDropdowns(companyId);
        }
        catch (Exception)
        {
            throw;
        }
    }


}
