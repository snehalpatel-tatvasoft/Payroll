namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.PayslipDisplaySetup;

public class PayslipDisplaySetupDataDTO
{
    public long CompanyId { get; set; }
    public bool IsCompanyContribution { get; set; }
    public bool IsFringeBenefits { get; set; }
    public int PayslipLayout { get; set; }
    public string PayslipMessage { get; set; }="";
}
