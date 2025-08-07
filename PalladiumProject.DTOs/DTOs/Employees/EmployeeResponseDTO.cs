namespace PalladiumPayroll.DTOs.DTOs.Employees
{
	public class EmployeeDataViewModel
	{
		public string EmployeeId { get; set; }
		public string EmployeeCode { get; set; }
		public string EmployeeName { get; set; }
		public string? ProfileUrl { get; set; }
		public string IDNumber { get; set; }
		public DateTime Dob { get; set; }
		public string? PayrollCycle { get; set; }
		public string? Department { get; set; }
		public string? Designation { get; set; }
	}

	public class EmployeeOrgnizationalModel
	{
		public int CompanyId { get; set; }
		public int EmployeeId { get; set; }
		public DateTime? StartDate { get; set; }
		public int? DesignationId { get; set; }
		public int? JobGradeId { get; set; }
		public int? OccupationalLevelId { get; set; }
		public int? OccupationalStatusId { get; set; }
		public int? OccupationalCategoryId { get; set; }
		public int? WSPCategoryId { get; set; }
		public int? OFOCodeId { get; set; }
		public int? MajorCostCenterId { get; set; }
		public int? RegionId { get; set; }
		public int? AppointmentTypeId { get; set; }
		public int? PayPointId { get; set; }
		public int? NICGradeId { get; set; }
		public int? BranchId { get; set; }
		public int? DivisionId { get; set; }
		public int SubDivisionId { get; set; }
		public int? MunicipalityId { get; set; }
		public int? LocationId { get; set; }
		public int? DepartmentId { get; set; }
		public int? ProvinceId { get; set; }
		public int? SupportFunctionId { get; set; }
	}
   

	public class PayrollTransactionList
	{
		public int? TakeOnBalanceId { get; set; }
		public int? PayrollProcessId { get; set; }
		public string Description { get; set; }
		public decimal? Amount { get; set; }
	}

	public class TakeOnBalanceTransaction
	{
		public int TakeOnBalanceId { get; set; }
		public string Description { get; set; }
		public decimal? Amount { get; set; }
	}

	public class TakeOnBalanceListWithTakeOnComplete
	{
		public List<TakeOnBalanceTransaction> TakeBalanceList { get; set; } = new();
		public bool TakeOnComplete { get; set; }
	}

	public class TransactionType
	{
		public long PayrollProcessId { get; set; }
		public string Description { get; set; } = "";
	}
    public class TimeSheetSetup
    {
        public int EmployeeId { get; set; }
        public bool? EnableTimeSheet { get; set; }
        public string TimeSheetPassword { get; set; }
        public string TimeSheetConfirmPassword { get; set; }

    }
    


    public class TransactionTypeDropdownsDTO
    {
        public List<TransactionType> TransactionType { get; set; } = new();
    }
    public class GetDirectiveResponse
    {
        public int Id { get; set; }
        public string DirectiveNumber { get; set; } = string.Empty;
        public DateTime DirectiveDate { get; set; }
        public int SourceCode { get; set; }
        public decimal DirectiveAmount { get; set; }
        public string TypeIndicator { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public bool Status { get; set; }
    }

	public class EmployeeLoanInfoDto
	{
		public long EmployeeLoanId { get; set; }
		public long EmployeeId { get; set; }
		public string EmployeeCode { get; set; } = "";
		public string EmployeeName { get; set; } = "";
		public DateTime RepaymentStartDate { get; set; }
		public DateTime LoanGrantDate { get; set; }
		public int NumberOfRepayment { get; set; }
		public decimal CurrentRepaymentAmount { get; set; }
		public decimal LoanAmount { get; set; }
		public decimal LoanPaidAmount { get; set; }
		public decimal OutstandingAmount { get; set; }
		public string LoanStatusName { get; set; } = "";
		public decimal LoanMaxDeduction { get; set; }
		public decimal InterestRate { get; set; }
		public decimal TotalLoanAmount { get; set; }
		public string LoanIntegration { get; set; } = "";
		public bool IsPaushed { get; set; }
	}

	public class LoanSummaryDto
	{
		public decimal TotalOutstandingAmount { get; set; }
		public int TotalLoanTransactions { get; set; }
	}

	public class EmployeeLoanResponse
	{
		public List<EmployeeLoanInfoDto> Loans { get; set; } = new List<EmployeeLoanInfoDto>();
		public LoanSummaryDto Summary { get; set; } = new LoanSummaryDto();
	}

	public class AccountTypeDto
	{
		public int AccountTypeId { get; set; }
		public string AccountTypeName { get; set; } = "";
	}

	public class BankDto
	{
		public int BankId { get; set; }
		public string BankName { get; set; } = "";
		public string BranchCode { get; set; } = "";
	}

	public class GarnisheeDropdownListDto
	{
		public List<AccountTypeDto> AccountTypes { get; set; } = new List<AccountTypeDto>();
		public List<BankDto> Banks { get; set; } = new List<BankDto>();
	}

	public class GarnishDetails
	{
		public long GarnishesId { get; set; }
		public long EmployeeId { get; set; }
		public DateTime? GarnishesStartDate { get; set; }
		public decimal? GarnishesAmount { get; set; }
		public int? NumberOfRepayment { get; set; }
		public decimal? CurrentRepayment { get; set; }
		public decimal? ClosingBalance { get; set; }
		public decimal? GarnishesPaidAmount { get; set; }
		public DateTime? GarnishesPaidStartDate { get; set; }
		public DateTime? GarnishesPaidEndDate { get; set; }
		public string AccountName { get; set; } = "";
		public int? AccountTypeId { get; set; }
		public string? AccountTypeName { get; set; }
		public int? BankId { get; set; }
		public string? BankName { get; set; }
		public string BranchCode { get; set; } = "";
		public int? IsLinkAccount { get; set; }
		public string AccountNumber { get; set; } = "";
	}

	public class SavingsDetails
	{
		public long SavingId { get; set; }
		public long EmployeeId { get; set; }
		public DateTime SavingsStartDate { get; set; }
		public DateTime? SavingsEndDate { get; set; }
		public decimal SavingsAmount { get; set; }
		public decimal CurrentRepayment { get; set; }
		public long NumberOfRepayment { get; set; }
		public decimal? ClosingBalance { get; set; }
	}

    public class TaxMethod
    {
        public long TaxMethodId { get; set; }
        public string? TaxMethodName { get; set; }
    }

    public class IT3aReasonCode
    {
        public long IT3aReasonCodeId { get; set; }
        public string? Name { get; set; }
    }
    public class UIFExempts
    {
        public long UIFExemptId { get; set; }
        public string? UIFExemptCode { get; set; }
        public string? UIFExemptName { get; set; }
    }

    public class TaxInformationDropdownData
    {
        public List<TaxMethod> TaxMethod { get; set; } = new();
        public List<IT3aReasonCode> IT3aReasonCode { get; set; } = new();
        public List<UIFExempts> UIFExempts { get; set; } = new();


    }
    public class TaxInformation
    {
        public long EmployeeId { get; set; }
        public string? IncomeTaxNumber { get; set; }
        public string? TaxOffice { get; set; }
        public int TaxMethod { get; set; }
        public int IT3aReasonCodes { get; set; }
        public int ExemptFromUIF { get; set; }
        public int MedicalAidBeneficiaries { get; set; }
        public bool IsOIDReportExclude { get; set; }
        public bool IsSDLExempt { get; set; }
        public bool IsPrivateBenefit { get; set; }
        public bool IsCompanyorClose { get; set; }
        public bool IsTrust { get; set; }
        public bool IsETIQualifies { get; set; }
        public decimal MinimumWage { get; set; }
        public string? ValidId { get; set; }
        public bool IsAverageWorkingHours { get; set; }
        

	}



	public class EmployeeDetailForEmployeeSelfservice
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public int? AccessRoleID { get; set; }
		public string AccessRoleName { get; set; }
		public int FunctionalityId { get; set; }
		public string FunctionalityName { get; set; }
		public bool? View { get; set; }
		public bool? Edit { get; set; }
		public bool? Delete { get; set; }
		public bool? IsManager { get; set; }
		public int? SecondApprovalId { get; set; }
		public string SecondApprovalFullName { get; set; }
	}

	public class FunctionalityPermissionDto
	{
		public int FunctionalityId { get; set; }
		public string FunctionalityName { get; set; }
		public bool? View { get; set; }
		public bool? Edit { get; set; }
		public bool? Delete { get; set; }
	}

	public class EmployeeSelfServiceResponse
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public int? AccessRoleID { get; set; }
		public string AccessRoleName { get; set; }
		public bool? IsManager { get; set; }
		public int? SecondApprovalId { get; set; }
		public string SecondApprovalFullName { get; set; }

		public List<FunctionalityPermissionDto> Functionalities { get; set; }
	}

	public class SecondApprovalEmployeeDTO
	{
		public long EmployeeId { get; set; }
		public string EmployeeFullname { get; set; }
	}

	public class AccessRoleDto
	{
		public int AccessRoleId { get; set; }
		public string AccessRoleName { get; set; }
	}

}
