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

    public class TimeSheetSetup
    {
        public int EmployeeId { get; set; }
        public bool? EnableTimeSheet { get; set; }
        public string TimeSheetPassword { get; set; }
        public string TimeSheetConfirmPassword { get; set; }

    }
    public class TransactionType
    {
        public long PayrollProcessId { get; set; }
        public string Description { get; set; } = "";
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
}
