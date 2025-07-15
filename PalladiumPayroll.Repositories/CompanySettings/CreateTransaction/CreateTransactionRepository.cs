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

    public async Task<bool> CheckDuplicateTransaction(long companyId, int allowanceTypeId, string description,  long? payrollProcessId=null)
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



}
