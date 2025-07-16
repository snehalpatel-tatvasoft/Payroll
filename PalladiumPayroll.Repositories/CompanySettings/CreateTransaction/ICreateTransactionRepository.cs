using PalladiumPayroll.DTOs.DTOs.CompanySettings.CreateTransaction;

namespace PalladiumPayroll.Repositories.CompanySettings.CreateTransaction;

public interface ICreateTransactionRepository
{
    Task<List<CreateTransactionResponseDTO>> GetAllTransactions(long companyId);
    Task<bool> AddTransaction(CreateTransactionRequestDTO request);

    Task<bool> UpdateTransaction(CreateTransactionRequestDTO request);
    Task<bool> CheckDuplicateTransaction(long companyId, int allowanceTypeId, string description, long? payrollProcessId = null);
    Task<CreateTransactionResponseDTO?> GetTransactionById(long payrollProcessId);
    Task<bool> DeleteTransaction(long payrollProcessId);

}
