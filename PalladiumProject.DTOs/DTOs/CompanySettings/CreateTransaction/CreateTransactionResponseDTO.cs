namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.CreateTransaction;

public class CreateTransactionResponseDTO
{
    public long PayrollProcessId { get; set; }
    public string TransactionType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string IRP5_Code { get; set; } = null!;
    public string IRP5_Description { get; set; } = null!;
    public string TaxMethod { get; set; } = null!;
    public decimal? TaxablePercent { get; set; }
    public bool DirectiveRequired { get; set; }
    public bool UIF { get; set; }
    public decimal? UifPercent { get; set; }
    public bool SDL { get; set; }
    public decimal? SdlPercent { get; set; }
    public bool OID { get; set; }
    public bool RFI { get; set; }
    public decimal? RfiPercent { get; set; }
    public bool ETI { get; set; }
    public bool? EtiUsedPercentValue { get; set; }
    public bool Enable { get; set; }
    public string BCEA { get; set; } = null!;
    public bool SpecialRun { get; set; }
    public bool Shifts { get; set; }
    public bool LeaveWeeks { get; set; }
    public bool EssClaim { get; set; }
    public decimal? Percentage { get; set; }
    public string CalculationType { get; set; } = null!;
    public decimal? Amount { get; set; }
    public bool AffectTaxBenefitOnly { get; set; }
    public long? Council { get; set; }
    public int ShiftCalculationType { get; set; }
    public int Factor { get; set; }
    public int NumberOfWeeks { get; set; }

}
