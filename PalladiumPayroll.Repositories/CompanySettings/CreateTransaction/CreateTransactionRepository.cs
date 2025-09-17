using System.Data;
using Dapper;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.CreateTransaction;

namespace PalladiumPayroll.Repositories.CompanySettings.CreateTransaction;

public class CreateTransactionRepository : ICreateTransactionRepository
{
    private readonly DapperContext _dapper;

    public CreateTransactionRepository(DapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<List<CreateTransactionResponseDTO>> GetAllTransactions(long companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        var result = await _dapper.ExecuteStoredProcedure<CreateTransactionResponseDTO>(
            "usp_GetAllTransactions", parameters);

        return result.ToList();
    }

    public async Task<bool> CheckDuplicateTransaction(long companyId, int allowanceTypeId, string description, long? payrollProcessId = null)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@AllowanceTypeId", allowanceTypeId);
        parameters.Add("@Description", description);
        parameters.Add("@PayrollProcessId", payrollProcessId);


        var isDuplicate = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CheckDuplicateTransaction", parameters);
        return isDuplicate;
    }

    public async Task<bool> AddTransaction(CreateTransactionRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@AllowanceTypeId", request.TransactionType);
        parameters.Add("@Description", request.Description);
        parameters.Add("@IRP5_Code", request.IRP5Code);
        parameters.Add("@Taxability", request.TaxMethod);
        parameters.Add("@Taxable_Percent", request.TaxablePercent);
        parameters.Add("@Directive_Required", request.DirectiveRequired);
        parameters.Add("@UIF", request.UIF);
        parameters.Add("@UIFPercent", request.UIFPercent);
        parameters.Add("@SDL", request.SDL);
        parameters.Add("@SDLPercent", request.SDLPercent);
        parameters.Add("@COID", request.COID);
        parameters.Add("@RFI", request.RFI);
        parameters.Add("@RFIPercent", request.RFIPercent);
        parameters.Add("@ETI", request.ETI);
        parameters.Add("@ETI_Used_Taxable_Value", request.ETIUsedTaxableValue);
        parameters.Add("@AffectTaxOnly", request.AffectTaxBenefitOnly);
        parameters.Add("@EssClaim", request.EssClaim);
        parameters.Add("@SpecialRun", request.SpecialRun);
        parameters.Add("@TransactionEnable", request.Enable);
        parameters.Add("@CouncilOptionsId", request.Council);
        parameters.Add("@Amount", request.Amount);
        parameters.Add("@CalculationType", string.IsNullOrWhiteSpace(request.CalculationType) ? null : request.CalculationType);
        parameters.Add("@Percentage", request.Percentage);
        parameters.Add("@BCEA", string.IsNullOrWhiteSpace(request.BCEA) ? null : request.BCEA);
        parameters.Add("@Shifts", request.Shifts);
        parameters.Add("@LeaveWeeks", request.LeaveWeeks);
        parameters.Add("@NumberOfWeeks", request.NumberOfWeeks);
        parameters.Add("@Factor", request.Factor);
        parameters.Add("@ShiftCalculationTypeId", request.ShiftCalculationType);

        var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_InsertPayrollTransaction", parameters);
        return result;
    }

    public async Task<bool> UpdateTransaction(CreateTransactionRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@PayrollProcessId", request.PayrollProcessId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@AllowanceTypeId", request.TransactionType);
        parameters.Add("@Description", request.Description);
        parameters.Add("@IRP5_Code", request.IRP5Code);
        parameters.Add("@Taxability", request.TaxMethod);
        parameters.Add("@Taxable_Percent", request.TaxablePercent);
        parameters.Add("@Directive_Required", request.DirectiveRequired);
        parameters.Add("@UIF", request.UIF);
        parameters.Add("@UIFPercent", request.UIFPercent);
        parameters.Add("@SDL", request.SDL);
        parameters.Add("@SDLPercent", request.SDLPercent);
        parameters.Add("@COID", request.COID);
        parameters.Add("@RFI", request.RFI);
        parameters.Add("@RFIPercent", request.RFIPercent);
        parameters.Add("@ETI", request.ETI);
        parameters.Add("@ETI_Used_Taxable_Value", request.ETIUsedTaxableValue);
        parameters.Add("@AffectTaxOnly", request.AffectTaxBenefitOnly);
        parameters.Add("@EssClaim", request.EssClaim);
        parameters.Add("@TransactionEnable", request.Enable);
        parameters.Add("@CouncilOptionsId", request.Council);
        parameters.Add("@Amount", request.Amount);
        parameters.Add("@CalculationType", request.CalculationType);
        parameters.Add("@Percentage", request.Percentage);
        parameters.Add("@BCEA", request.BCEA);
        parameters.Add("@Shifts", request.Shifts);
        parameters.Add("@LeaveWeeks", request.LeaveWeeks);
        parameters.Add("@NumberOfWeeks", request.NumberOfWeeks);
        parameters.Add("@Factor", request.Factor);
        parameters.Add("@ShiftCalculationTypeId", request.ShiftCalculationType);
        parameters.Add("@SpecialRun", request.SpecialRun);

        var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdatePayrollTransaction", parameters);
        return result;
    }
    public async Task<CreateTransactionResponseDTO?> GetTransactionById(long payrollProcessId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@PayrollProcessId", payrollProcessId);

        var result = await _dapper.ExecuteStoredProcedure<CreateTransactionResponseDTO>(
            "usp_GetTransactionById", parameters);

        return result.FirstOrDefault();
    }
    public async Task<bool> DeleteTransaction(long id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteTransaction", parameters);
    }

    // public async Task<string?> ImportTransactions(CreateTransactionRequestDTO transaction)
    // {
    //     var parameters = new DynamicParameters();
    //     parameters.Add("@CompanyId", transaction.CompanyId);
    //     parameters.Add("@AllowanceTypeId", transaction.TransactionType);
    //     parameters.Add("@Description", transaction.Description);
    //     parameters.Add("@IRP5_Code", transaction.IRP5Code);
    //     parameters.Add("@Taxability", transaction.TaxMethod);
    //     parameters.Add("@Taxable_Percent", transaction.TaxablePercent);
    //     parameters.Add("@Directive_Required", transaction.DirectiveRequired);
    //     parameters.Add("@UIF", transaction.UIF);
    //     parameters.Add("@UIFPercent", transaction.UIFPercent);
    //     parameters.Add("@SDL", transaction.SDL);
    //     parameters.Add("@SDLPercent", transaction.SDLPercent);
    //     parameters.Add("@COID", transaction.COID);
    //     parameters.Add("@RFI", transaction.RFI);
    //     parameters.Add("@RFIPercent", transaction.RFIPercent);
    //     parameters.Add("@ETI", transaction.ETI);
    //     parameters.Add("@ETI_Used_Taxable_Value", transaction.ETIUsedTaxableValue);
    //     parameters.Add("@AffectTaxOnly", transaction.AffectTaxBenefitOnly);
    //     parameters.Add("@EssClaim", transaction.EssClaim);
    //     parameters.Add("@SpecialRun", transaction.SpecialRun);
    //     parameters.Add("@TransactionEnable", transaction.Enable);
    //     parameters.Add("@CouncilOptionsId", transaction.Council);
    //     parameters.Add("@Amount", transaction.Amount);
    //     parameters.Add("@CalculationType", string.IsNullOrWhiteSpace(transaction.CalculationType) ? null : transaction.CalculationType);
    //     parameters.Add("@Percentage", transaction.Percentage);
    //     parameters.Add("@BCEA", string.IsNullOrWhiteSpace(transaction.BCEA) ? null : transaction.BCEA);
    //     parameters.Add("@Shifts", transaction.Shifts);
    //     parameters.Add("@LeaveWeeks", transaction.LeaveWeeks);
    //     parameters.Add("@NumberOfWeeks", transaction.NumberOfWeeks);
    //     parameters.Add("@Factor", transaction.Factor);
    //     parameters.Add("@ShiftCalculationTypeId", transaction.ShiftCalculationType);

    //     var result = await _dapper.ExecuteStoredProcedureSingle<string>("usp_ImportTransactions", parameters);
    //     return result;
    // }
    public async Task<string?> ImportTransactions(ImportTransactionRequestDTO request)
    {
        var table = TransactionsToDataTable(request.Transactions);

        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@Transactions", table.AsTableValuedParameter("dbo.TransactionImportType"));

        var result = await _dapper.ExecuteStoredProcedureSingle<string>("usp_ImportTransactions", parameters);

        if (result?.StartsWith("ERROR") == true)
            return result;

        return result;
    }

    private DataTable TransactionsToDataTable(List<CreateTransactionRequestDTO> transactions)
    {
        var table = new DataTable();
        table.Columns.Add("AllowanceTypeId", typeof(int));
        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("IRP5_Code", typeof(long));
        table.Columns.Add("Taxability", typeof(string));
        table.Columns.Add("Taxable_Percent", typeof(int));
        table.Columns.Add("Directive_Required", typeof(bool));
        table.Columns.Add("UIF", typeof(bool));
        table.Columns.Add("UIFPercent", typeof(int));
        table.Columns.Add("SDL", typeof(bool));
        table.Columns.Add("SDLPercent", typeof(int));
        table.Columns.Add("COID", typeof(bool));
        table.Columns.Add("RFI", typeof(bool));
        table.Columns.Add("RFIPercent", typeof(int));
        table.Columns.Add("ETI", typeof(bool));
        table.Columns.Add("ETI_Used_Taxable_Value", typeof(bool));
        table.Columns.Add("EssClaim", typeof(bool));
        table.Columns.Add("SpecialRun", typeof(bool));
        table.Columns.Add("AffectTaxOnly", typeof(bool));
        table.Columns.Add("CouncilOptionsId", typeof(long));
        table.Columns.Add("Amount", typeof(decimal));
        table.Columns.Add("CalculationType", typeof(string));
        table.Columns.Add("Percentage", typeof(decimal));
        table.Columns.Add("BCEA", typeof(string));
        table.Columns.Add("Shifts", typeof(bool));
        table.Columns.Add("LeaveWeeks", typeof(bool));
        table.Columns.Add("NumberOfWeeks", typeof(int));
        table.Columns.Add("Factor", typeof(decimal));
        table.Columns.Add("ShiftCalculationTypeId", typeof(int));
        table.Columns.Add("TransactionEnable", typeof(bool));

        foreach (var transaction in transactions)
        {
            table.Rows.Add(
                transaction.TransactionType,
                transaction.Description ?? (object)DBNull.Value,
                transaction.IRP5Code ?? (object)DBNull.Value,
                transaction.TaxMethod ?? (object)DBNull.Value,
                transaction.TaxablePercent ?? (object)DBNull.Value,
                transaction.DirectiveRequired,
                transaction.UIF,
                transaction.UIFPercent ?? (object)DBNull.Value,
                transaction.SDL,
                transaction.SDLPercent ?? (object)DBNull.Value,
                transaction.COID,
                transaction.RFI,
                transaction.RFIPercent ?? (object)DBNull.Value,
                transaction.ETI,
                transaction.ETIUsedTaxableValue,
                transaction.EssClaim,
                transaction.SpecialRun,
                transaction.AffectTaxBenefitOnly,
                transaction.Council ?? (object)DBNull.Value,
                transaction.Amount ?? (object)DBNull.Value,
                string.IsNullOrWhiteSpace(transaction.CalculationType) ? (object)DBNull.Value : transaction.CalculationType,
                transaction.Percentage ?? (object)DBNull.Value,
                string.IsNullOrWhiteSpace(transaction.BCEA) ? (object)DBNull.Value : transaction.BCEA,
                transaction.Shifts,
                transaction.LeaveWeeks,
                transaction.NumberOfWeeks ?? (object)DBNull.Value,
                transaction.Factor ?? (object)DBNull.Value,
                transaction.ShiftCalculationType ?? (object)DBNull.Value,
                transaction.Enable
            );
        }

        return table;
    }

}
