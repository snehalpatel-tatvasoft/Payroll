using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.CreateTransaction;

namespace PalladiumPayroll.Services.CompanySettings.CreateTransaction;

public interface ICreateTransactionService
{
    Task<JsonResult> GetAllTransactions(long companyId);
    Task<JsonResult> AddTransaction(CreateTransactionRequestDTO request);
    Task<JsonResult> UpdateTransaction(CreateTransactionRequestDTO request);
    Task<JsonResult> GetTransactionById(long payrollProcessId);
    Task<JsonResult> DeleteTransaction(long payrollProcessId);

    
}
