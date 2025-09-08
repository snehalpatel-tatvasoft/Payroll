using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.CreateTransaction;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings.CreateTransaction;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;


namespace PalladiumPayroll.Services.CompanySettings.CreateTransaction;

public class CreateTransactionService : ICreateTransactionService
{
    private readonly ICreateTransactionRepository _createTransactionRepository;

    public CreateTransactionService(ICreateTransactionRepository createTransactionRepository)
    {
        _createTransactionRepository = createTransactionRepository;
    }

    public async Task<JsonResult> GetAllTransactions(long companyId)
    {
        var transactions = await _createTransactionRepository.GetAllTransactions(companyId);
        return HttpStatusCodeResponse.SuccessResponse(transactions,string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Retrieved));
    }

    public async Task<JsonResult> AddTransaction(CreateTransactionRequestDTO request)
    {
        bool isDuplicate = await _createTransactionRepository.CheckDuplicateTransaction(
                     request.CompanyId,
                     request.TransactionType,
                     request.Description
                 );

        if (isDuplicate)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DuplicateTransaction);
        }

        bool isCreated = await _createTransactionRepository.AddTransaction(request);

        if (isCreated)
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Saved));
        }

        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TransactionCreationFailed);
    }

    public async Task<JsonResult> UpdateTransaction(CreateTransactionRequestDTO request)
    {

        bool isDuplicate = await _createTransactionRepository.CheckDuplicateTransaction(
            request.CompanyId,
            request.TransactionType,
            request.Description,
            request.PayrollProcessId
        );

        if (isDuplicate)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DuplicateTransaction);
        }

        bool isUpdated = await _createTransactionRepository.UpdateTransaction(request);
        if (isUpdated)
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Updated));
        }

        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TransactionUpdateFailed);

    }
    public async Task<JsonResult> GetTransactionById(long payrollProcessId)
    {

        var transaction = await _createTransactionRepository.GetTransactionById(payrollProcessId);
        return HttpStatusCodeResponse.SuccessResponse(transaction, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Retrieved));

    }
    public async Task<JsonResult> DeleteTransaction(long id)
    {

        bool result = await _createTransactionRepository.DeleteTransaction(id);
        if (result)
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Deleted));
        }
        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TransactionDeleteFailed);

    }
    public async Task<JsonResult> ImportTransactions(ImportTransactionRequestDTO request)
    {
        var status = await _createTransactionRepository.ImportTransactions(request);

        if (status?.StartsWith("ERROR") == true)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(status ?? ResponseMessages.TransactionImportFailed);
        }

        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Imported));
    }

}
