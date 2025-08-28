namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.LeaveSettings;

public class LeaveSettingsResponseDTO
{
    public int LeaveRuleId { get; set; }
    public int RuleID { get; set; }
    public int LeaveTypeID { get; set; }
    public string CycleName { get; set; } = string.Empty;
    public string? PeriodStart { get; set; }
    public int? Duration { get; set; }

    // Always populated for cases 4,5,6,8
    public decimal? AnnualEntitlementDays { get; set; }

    // Only populated for cases 1,2,3,7
    public string? LeaveAccumulationPeriod { get; set; }
    public decimal? LeaveAccumulationDays { get; set; }
    public int? ExceedDue { get; set; }
    public bool? LeaveCarriedForward { get; set; }
    public int? LeaveCarriedForwardMaxDays { get; set; }
    public bool? Recurring { get; set; }
    public int? NoOfTimeReccuring { get; set; }
}
