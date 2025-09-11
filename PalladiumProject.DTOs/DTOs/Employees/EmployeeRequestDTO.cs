using Microsoft.AspNetCore.Http;
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

    public class EmployeePersonalInformation
    {
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeSurname { get; set; }
        public int? Title { get; set; }
        public string? Initials { get; set; }
        public string? ProfilePicture { get; set; }
        public int? Gender { get; set; }
        public string? PreferredName { get; set; }
        public int? PassportIssuedBy { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? HomeNumber { get; set; }
        public string? CellNumber { get; set; }
        public string? Email { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyRelation { get; set; }
        public string? EmergencyCellNumber { get; set; }
        public int? HomeLanguage { get; set; }
        public int? Profile { get; set; }
        public string? IDNumber { get; set; }
        public string? PassportNumber { get; set; }
        public int? Race { get; set; }
        public int? EmploymentStatus { get; set; }
        public int? NatureOfPerson { get; set; }
        public bool IsPersonwithDisability { get; set; }
        public bool IsForeignNational { get; set; }
        public bool IsRefugee { get; set; }
        public bool IsAsylumSeeker { get; set; }
        public string? AsylumPermitNumber { get; set; }
        public int? AddressIndicator { get; set; }
        public string? UnitNumber { get; set; }
        public string? ComplexName { get; set; }
        public string? StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Phy_PostalCode { get; set; }
        public int? Phy_CountryId { get; set; }
        public bool IsPostalSame { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? Pos_PostalCode { get; set; }
        public int? Pos_CountryId { get; set; }
        public string? UserId { get; set; }
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
        public decimal? RatePerDay { get; set; }
        public decimal? RatePerHour { get; set; }
        public List<int>? WorkingDay { get; set; }
        public string? StandardWorkingDays { get; set; }
        public decimal? HoursPerMonth { get; set; }
        public decimal? HoursPerWeek { get; set; }
        public decimal? HoursPerDay { get; set; }
        public decimal? DayPerMonth { get; set; }
        public decimal? DayPerWeek { get; set; }
        public int? MinimumWage { get; set; }
        public decimal? LeavePeriod { get; set; }
        public decimal? LeaveYear { get; set; }
        public decimal? BonusPeriod { get; set; }
        public decimal? BonusYear { get; set; }
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
        public string? AccountName { get; set; } = "";
        public string? AccountNumber { get; set; }
        public long? AccountTypeId { get; set; }
        public long? BankId { get; set; }
        public string? BranchCode { get; set; } = "";
    }

    public class EmployeeSavingsRequest
    {
        public long? SavingsId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime SavingsStartDate { get; set; }
        public decimal SavingsAmount { get; set; }
        public long NumberOfRepayment { get; set; }
        public decimal CurrentRepayment { get; set; }
    }

    public class EmployeeDocumentUpload
    {
        public int EmployeeId { get; set; }
        public List<IFormFile> Document { get; set; }
    }

    public class EmployeeDocuments
    {
        public int? DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentUrl { get; set; }
        public long DocumentSize { get; set; }
        public string DocumentType { get; set; }
    }

    public class EmployeeDocumentDelete
    {
        public int EmployeeId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentUrl { get; set; }
    }

    public class FunctionalityUpdate
    {
        public int FunctionalityId { get; set; }
        public bool View { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }

    public class UpdateEmployeeSelfServiceModel
    {
        public long EmployeeId { get; set; }
        public long CompanyId { get; set; }
        public List<FunctionalityUpdate> FunctionalityList { get; set; }=new List<FunctionalityUpdate>();
        public bool IsManager { get; set; }
        public int? SecondApprovalId { get; set; }
        public List<long> EmployeeAssignList { get; set; } = new List<long>();
    }

    public class UpsertUserRequestDTO
    {
        public int AccessRoleId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public long CompanyId { get; set; }
        public long EmployeeId { get; set; }
    }
    public class LeaveModel
    {
        public int Id { get; set; }
        public string LeaveType { get; set; } = string.Empty;
        public decimal TakeOnBalance { get; set; }
        public decimal DaysAccrued { get; set; }
        public decimal DaysTaken { get; set; }
        public decimal DaysDue { get; set; }
        public decimal CycleLeaveEntitlement { get; set; }
        public int LeaveTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int Year { get; set; }
    }

    public class EditLeaveRequest
    {
        public int EmployeeId { get; set; }
        public int? EmployeeLeaveId { get; set; }
        public int LeaveTypeId { get; set; }
        public int? Year { get; set; }
        public decimal TakeOnBalance { get; set; }
        public decimal DaysAccrued { get; set; }
        public decimal DaysTaken { get; set; }
        public decimal DaysDue { get; set; }
        public decimal CycleLeaveEntitlement { get; set; }
    }
}
