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

	public class TimeSheetSetup
	{
		public int EmployeeId { get; set; }
		public bool? EnableTimeSheet { get; set; }
		public string TimeSheetPassword { get; set; }
		public string TimeSheetConfirmPassword { get; set; }

	}

	public class TransactionList
	{
        public int? PayrollProcessId { get; set; }
		public int? TakeOnBalanaceId { get; set; }
        public string Description { get; set; }
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


}
