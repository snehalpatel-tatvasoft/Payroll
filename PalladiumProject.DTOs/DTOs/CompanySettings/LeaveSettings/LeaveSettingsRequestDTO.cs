namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.LeaveSettings;

public class LeaveSettingsRequestDTO
{
    public int LeaveRuleId { get; set; }
    public int CaseId { get; set; }
    
    public int? Duration { get; set; }
    public decimal? LeaveAccumulationDays { get; set; }
    public int? ExceedDue { get; set; }
    public bool? LeaveCarriedForward { get; set; }
    public int? LeaveCarriedForwardMaxDays { get; set; }
    public bool? Recurring { get; set; }
    public int? NoOfTimeReccuring { get; set; }
    public decimal? AnnualEntitlementDays { get; set; }
}


