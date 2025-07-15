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
            bool isUpdated = await _createTransactionRepository.UpdateTransaction(request);
            if (isUpdated)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.TransactionUpdatedSuccessfully);
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TransactionUpdateFailed);
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                success = false,
                error = ex.Message,
                inner = ex.InnerException?.Message,
                stack = ex.StackTrace
            });
        }
    }

}
