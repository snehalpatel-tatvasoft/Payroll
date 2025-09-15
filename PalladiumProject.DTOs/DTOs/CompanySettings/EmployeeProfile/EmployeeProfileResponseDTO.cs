namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

public class EmployeeProfileResponseDTO
{
}

public class LeaveRulesListDTO
{
    public int LeaveRuleId { get; set; }
    public int RuleID { get; set; }
    public int LeaveTypeID { get; set; }
    public string CycleName { get; set; } = string.Empty;
    public string? PeriodStart { get; set; }
    public int? Duration { get; set; }
    public decimal? AnnualEntitlementDays { get; set; }
    public string? LeaveAccumulationPeriod { get; set; }
    public decimal? LeaveAccumulationDays { get; set; }
    public int? ExceedDue { get; set; }
    public bool? LeaveCarriedForward { get; set; }
    public decimal? LeaveCarriedForwardMaxDays { get; set; }
    public bool? Recurring { get; set; }
    public int? NoOfTimeReccuring { get; set; }
}

public class EmployeeProfileListDTO
{
    public long ProfileId { get; set; }   
    public string ProfileName { get; set; } = string.Empty;
}

public class EmployeeProfileDetailsDTO
{
    public long ProfileId { get; set; }
    public string ProfileName { get; set; }=string.Empty;
    public long? CompanyId { get; set; }
    public long? DepartmentId { get; set; }
    public long? DesignationId { get; set; }
    public int? PayrollCycleTypeId { get; set; }
    public decimal? AnnualSalary { get; set; }
    public decimal? MonthlySalary { get; set; }
    public decimal? RatePerDay { get; set; }
    public decimal? RatePerHour { get; set; }
    public string? StandardWorkingDays { get; set; }
    public int? HoursPerMonth { get; set; }
    public int? HoursPerWeek { get; set; }
    public int? HoursPerDay { get; set; }
    public int? DaysPerMonth { get; set; }
    public int? DaysPerWeek { get; set; }
    public DateTime? StartDate { get; set; }
    public long? ApprovalId { get; set; }
    public long? EmployeeDefaultWorkDetailsId { get; set; }
}

public class TransactionListModel
{
    public int PayrollProcessId { get; set; }
    public string Description { get; set; }
}