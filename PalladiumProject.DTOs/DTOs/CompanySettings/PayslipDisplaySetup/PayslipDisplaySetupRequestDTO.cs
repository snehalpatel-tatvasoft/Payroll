namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.PayslipDisplaySetup;

public class SavePayslipSettingsDTO
{
    public long CompanyId { get; set; }
    public bool IsCompanyContribution { get; set; }
    public bool IsFringeBenefits { get; set; }
    public int PayslipLayout { get; set; }
    public string PayslipMessage { get; set; }="";
    public long UserId { get; set; }
     public int RestorePayslipLayout { get; set; } = 0;
}