using Newtonsoft.Json;

namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.CreateTransaction;

public class CreateTransactionRequestDTO
{
    public long PayrollProcessId { get; set; } 
    public string Description { get; set; } = null!; 
    public int TransactionType { get; set; } 
    public long? IRP5Code { get; set; }
    public string TaxMethod { get; set; } = null!; 
    public int? TaxablePercent { get; set; }
    public bool DirectiveRequired { get; set; }
    public bool UIF { get; set; }
    public int? UIFPercent { get; set; }
    public bool SDL { get; set; }
    public int? SDLPercent { get; set; }
    public bool COID { get; set; }
    public bool RFI { get; set; }
    public int? RFIPercent { get; set; }
    public bool ETI { get; set; }
    public bool ETIUsedTaxableValue { get; set; }
    public bool EssClaim { get; set; }
    public bool SpecialRun { get; set; }
    public bool AffectTaxBenefitOnly { get; set; }
    public string? BCEA { get; set; } 
    public decimal? Amount { get; set; } 
    public string? CalculationType { get; set; } 
    public decimal? Percentage { get; set; } 
    public bool Shifts { get; set; }
    public bool LeaveWeeks { get; set; }
    public int? NumberOfWeeks { get; set; }
    public decimal? Factor { get; set; }
    public int? ShiftCalculationType { get; set; }
    public bool Enable { get; set; } 
    public long CompanyId { get; set; }
    public long? Council { get; set; } 

}

public class ImportTransactionRequestDTO
{
    public long CompanyId { get; set; }
    public List<CreateTransactionRequestDTO> Transactions { get; set; } = new List<CreateTransactionRequestDTO>();
}