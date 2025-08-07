using PalladiumPayroll.DTOs.DTOs.Common;

namespace PalladiumPayroll.DTOs.DTOs.Employees
{
    public class EmployeeFilterViewModel : TableFilterViewModel
    {
        public int CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public bool? IsActive { get; set; } = true;
    }

    public class EmployeePaymentDetail
    {
        public int? EmployeeId { get; set; }
        public int PaymentMethod { get; set; }
        public string? AccountHolderName { get; set; }
        public string? AccountNumber { get; set; }
        public int? TypeofAccount { get; set; }
        public int? BankId { get; set; }
        public int? BranchCode { get; set; }
        public string? AccountHolderRelation { get; set; }
        public bool? SplitPayment { get; set; }
        public string? AccountHolderName1 { get; set; }
        public string? AccountNumber1 { get; set; }
        public int? TypeofAccount1 { get; set; }
        public int? BankId1 { get; set; }
        public int? BranchCode1 { get; set; }
        public string? AccountHolderRelation1 { get; set; }
        public decimal? SplitAmount1 { get; set; }
        public decimal? SplitAmount2 { get; set; }
        public decimal? SplitPercent1 { get; set; }
        public decimal? SplitPercent2 { get; set; }
    }

    public class EmployeeWorkInformation
    {
        public int EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public int? CycleType { get; set; }
        public int? ReportTo { get; set; }
        public bool? IsCommission { get; set; }
        public bool? IsExcludeEFA { get; set; }
        public long? RepCode { get; set; }
        public decimal? AnnualSalary { get; set; }
        public decimal? MonthlySalary { get; set; }
        public int? RatePerDay { get; set; }
        public int? RatePerHour { get; set; }
        public List<int>? WorkingDay { get; set; }
        public string? StandardWorkingDays { get; set; }
        public int? HoursPerMonth { get; set; }
        public int? HoursPerWeek { get; set; }
        public int? HoursPerDay { get; set; }
        public int? DayPerMonth { get; set; }
        public int? DayPerWeek { get; set; }
        public int? MinimumWage { get; set; }
        public int? LeavePeriod { get; set; }
        public int? LeaveYear { get; set; }
        public int? BonusPeriod { get; set; }
        public int? BonusYear { get; set; }
        public decimal? BCEAMonthlySalary { get; set; }
        public decimal? BCEAVariableSalary { get; set; }
        public int? BCEAOverMonth { get; set; }
        public decimal? BCEATotalRemuneration { get; set; }
        public decimal? BCEATermination { get; set; }
        public decimal? BCEAPortionLeavePay { get; set; }
    }

    public class WorkOrgnizationItem
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }

    public class CasualWageInformation
    {
        public int EmployeeId { get; set; }
        public decimal? NormalHour { get; set; }
        public decimal? CasualOverTime { get; set; }
        public decimal? HolidayRate { get; set; }
        public decimal? SundayRate { get; set; }
        public decimal? NightHour { get; set; }
        public decimal? CasualNightOvertime { get; set; }
        public decimal? HolidayNightRate { get; set; }
        public decimal? SundayNightRate { get; set; }
    }
    public class DirectiveRequest
    {
        public long EmployeeId { get; set; }
        public string DirectiveNumber { get; set; } = string.Empty;
        public DateTime DirectiveDate { get; set; }
        public int SourceCode { get; set; }
        public decimal Amount { get; set; }
        public string TypeIndicator { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public long? TransactionType { get; set; }
    }

    public class TransactionReqModel
    {
        public string? SearchName { get; set; }
        public int CompanyId { get; set; }
        public int AllowanceType { get; set; }
        public int? EmployeeId { get; set; }
    }

    public class TransactionSaveModel
    {
        public List<PayrollTransactionList> PayrollProcess { get; set; }
        public int AllowanceType { get; set; }
        public int EmployeeId { get; set; }
    }

    public class EmployeeGarnisheeRequest
    {
        public long? GarnishesId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime? GarnishesStartDate { get; set; }
        public decimal GarnishesAmount { get; set; }
        public long NumberOfRepayment { get; set; }
        public decimal CurrentRepayment { get; set; }
        public bool IsLinkAccount { get; set; }
        public string AccountName { get; set; } = "";
        public string? AccountNumber { get; set; }
        public long? AccountTypeId { get; set; }
        public long? BankId { get; set; }
        public string BranchCode { get; set; } = "";
        public long UserId { get; set; }
    }

    public class EmployeeSavingsRequest
    {
        public long? SavingsId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime SavingsStartDate { get; set; }
        public decimal SavingsAmount { get; set; }
        public long NumberOfRepayment { get; set; }
        public decimal CurrentRepayment { get; set; }
        public long UserId { get; set; }
    }
}
