using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.CreateTransaction;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings.CreateTransaction;
using static PalladiumPayroll.Helper.Constants.AppConstants;

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
        try
        {
            var transactions = await _createTransactionRepository.GetAllTransactions(companyId);
            return HttpStatusCodeResponse.SuccessResponse(transactions, ResponseMessages.DataFetchSuccess);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> AddTransaction(CreateTransactionRequestDTO request)
    {
        try
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
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.TransactionCreatedSuccessfully);
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TransactionCreationFailed);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> UpdateTransaction(CreateTransactionRequestDTO request)
    {
        try
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
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.TransactionUpdatedSuccessfully);
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TransactionUpdateFailed);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();

        }
    }
    public async Task<JsonResult> GetTransactionById(long payrollProcessId)
    {
        try
        {
            var transaction = await _createTransactionRepository.GetTransactionById(payrollProcessId);
            return HttpStatusCodeResponse.SuccessResponse(transaction, ResponseMessages.DataFetchSuccess);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }
    public async Task<JsonResult> DeleteTransaction(long id)
    {
        try
        {
            await _createTransactionRepository.DeleteTransaction(id);
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.TransactionDeletedSuccessfully);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }
    public async Task<JsonResult> ImportTransactions(ImportTransactionRequestDTO request)
    {
        try
        {
            foreach (var transaction in request.Transactions)
            {
                transaction.CompanyId = request.CompanyId;

                var status = await _createTransactionRepository.ImportTransactions(transaction);

                if (string.IsNullOrEmpty(status))
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Unknown error during import.");
                }
            }

            return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.TransactionImportedSuccessfully);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

}
