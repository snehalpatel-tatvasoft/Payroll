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

}
