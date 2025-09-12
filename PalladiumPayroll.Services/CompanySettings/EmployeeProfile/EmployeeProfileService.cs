using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings.EmployeeProfile;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;


namespace PalladiumPayroll.Services.CompanySettings.EmployeeProfile;

public class EmployeeProfileService : IEmployeeProfileService
{
    private readonly IEmployeeProfileRepository _employeeProfileRepository;

    public EmployeeProfileService(IEmployeeProfileRepository employeeProfileRepository)
    {
        _employeeProfileRepository = employeeProfileRepository;
    }
    public async Task<JsonResult> CreateProfile(EmployeeProfileRequestDTO request)
    {
        var (message, employeeProfileId) = await _employeeProfileRepository.CreateProfile(request);
        if (message == "Employee profile created successfully.")
        {
            return HttpStatusCodeResponse.SuccessResponse(new { EmployeeProfileId = employeeProfileId }, message);
        }

        return HttpStatusCodeResponse.InternalServerErrorResponse(message);
    }

    public async Task<JsonResult> GetWorkInformatiionDropdownData(int companyId)
    {
        return await _employeeProfileRepository.GetWorkInformatiionDropdownData(companyId);
    }
    public async Task<JsonResult> SaveWorkInformation(WorkInformationRequestDTO request)
    {
        try
        {
            var success = await _employeeProfileRepository.SaveWorkInformation(request);
            if (success)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.WorkInformation, ActionType.Saved));
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ResponseMessages.WorkInformation, ActionType.Saving));
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ResponseMessages.WorkInformation, ActionType.Saving, ex.Message));
        }
    }
    public async Task<JsonResult> GetModalTransactionsList(int transactionId)
    {
        try
        {
            var transactions = await _employeeProfileRepository.GetModalTransactionsList(transactionId);
            if (transactions.Any())
            {
                return HttpStatusCodeResponse.SuccessResponse(transactions, string.Format(ResponseMessages.Success, "Transaction list", ActionType.Retrieved));
            }

            return HttpStatusCodeResponse.SuccessResponse(new List<TransactionListModel>(), "No transactions found for the provided ID.");
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, "Transaction list", ActionType.Retrieving, ex.Message));
        }
    }
    public async Task<JsonResult> GetTransactionsList(int transactionId)
    {
        try
        {
            var transactions = await _employeeProfileRepository.GetTransactionsList(transactionId);
            if (transactions.Any())
            {
                return HttpStatusCodeResponse.SuccessResponse(transactions, string.Format(ResponseMessages.Success, "Transaction list", ActionType.Retrieved));
            }

            return HttpStatusCodeResponse.SuccessResponse(new List<TransactionListModel>(), "No transactions found for the provided ID.");
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, "Transaction list", ActionType.Retrieving, ex.Message));
        }
    }
    

}
