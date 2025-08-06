using PalladiumPayroll.DTOs.DTOs.Common;

namespace PalladiumPayroll.DTOs.HRFunctions.EmployeeTransfer;

public class JobGradeDto
{
    public long JobGradeId { get; set; }
    public string JobGradeName { get; set; } = "";
}

public class WSPCategoryDto
{
    public long WSPCategoryId { get; set; }
    public string WSPCategoryName { get; set; } = "";
}

public class OFOCodeDto
{
    public long OFOCodeId { get; set; }
    public string OFOCodeName { get; set; } = "";
}

public class MajorCostCenterDto
{
    public long MajorCostCenterId { get; set; }
    public string MajorCostCenterName { get; set; } = "";
}

public class NICGradeDto
{
    public long NICGradeId { get; set; }
    public string NICGradeName { get; set; } = "";
}

public class OccupationalCategoryDto
{
    public long OccupationalCategoryId { get; set; }
    public string OccupationalCategoryName { get; set; } = "";
}

public class OccupationalLevelDto
{
    public long OccupationalLevelId { get; set; }
    public string OccupationalLevelName { get; set; } = "";
}

public class BranchDto
{
    public long BranchId { get; set; }
    public string BranchName { get; set; } = "";
}

public class ProvinceDto
{
    public long ProvinceId { get; set; }
    public string ProvinceName { get; set; } = "";
}

public class SupportFunctionDto
{
    public long SupportFunctionId { get; set; }
    public string SupportFunctionName { get; set; } = "";
}

public class DepartmentDto
{
    public long DepartmentId { get; set; }
    public string DepartmentName { get; set; } = "";
}

public class EmployeeDropdownDto
{
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; } = "";
    public string EmployeeSurname { get; set; } = "";
    public string EmployeeCode { get; set; } = "";
    public string Initials { get; set; } = "";
}

public class DesignationCodeDto
{
    public long DesignationId { get; set; }
    public string DesignationCode { get; set; } = "";
}

public class OccupationalStatusDto
{
    public long OccupationalStatusId { get; set; }
    public string OccupationalStatusName { get; set; } = "";
}

public class AppointmentTypeDto
{
    public long AppointmentTypeId { get; set; }
    public string AppointmentTypeName { get; set; } = "";
}

public class DesignationDto
{
    public long DesignationId { get; set; }
    public string DesignationName { get; set; } = "";
}

public class EmployeeTransferDropdownsDTO
{
    public List<JobGradeDto> JobGrades { get; set; } = new();
    public List<WSPCategoryDto> WSPCategories { get; set; } = new();
    public List<OFOCodeDto> OFOCodes { get; set; } = new();
    public List<MajorCostCenterDto> MajorCostCenters { get; set; } = new();
    public List<NICGradeDto> NICGrades { get; set; } = new();
    public List<OccupationalCategoryDto> OccupationalCategories { get; set; } = new();
    public List<OccupationalLevelDto> OccupationalLevels { get; set; } = new();
    public List<BranchDto> Branches { get; set; } = new();
    public List<ProvinceDto> Provinces { get; set; } = new();
    public List<SupportFunctionDto> SupportFunctions { get; set; } = new();
    public List<DepartmentDto> Departments { get; set; } = new();
    public List<DropDownViewModel> Employees { get; set; } = new();
    public List<DesignationCodeDto> DesignationCodes { get; set; } = new();
    public List<OccupationalStatusDto> OccupationalStatuses { get; set; } = new();
    public List<AppointmentTypeDto> AppointmentTypes { get; set; } = new();
    public List<DesignationDto> Designations { get; set; } = new();
    public List<DropDownViewModel> ReportToEmployees { get; set; } = new();
}

public class EmployeeTransferResponseDTO
{
    public bool Result { get; set; }
    public string Message { get; set; } = "";
}

public class EmployeeTransferDetailDTO
{
    public long EmployeeTransferId { get; set; }
    public long EmployeeId { get; set; }
    public string? EmployeeInitialsSurname { get; set; }
    public long? DesignationId { get; set; }
    public long? JobGradeId { get; set; }
    public long? WSPCategoryId { get; set; }
    public long? OFOCodeId { get; set; }
    public long? MajorCostCenterId { get; set; }
    public long? NICGradeId { get; set; }
    public long? OccupationalCategoryId { get; set; }
    public long? OccupationalLevelId { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public long? ReportToId { get; set; }
    public long? BranchId { get; set; }
    public long? DepartmentId { get; set; }
    public long? ProvinceId { get; set; }
    public long? SupportFunctionId { get; set; }
    public long? OccupationalStatusId { get; set; }
    public long? AppointmentTypeId { get; set; }
    public long CompanyId { get; set; }
    public int UserId { get; set; } 
}

public class EmployeeTransferAutoFillDTO
{
    public string EmployeeInitialsSurname { get; set; } = string.Empty;
    public int? DesignationId { get; set; }
    public int? JobGradeId { get; set; }
    public int? WSPCategoryId { get; set; }
    public int? OFOCodeId { get; set; }
    public int? MajorCostCenterId { get; set; }
    public int? NICGradeId { get; set; }
    public int? OccupationalCategoryId { get; set; }
    public int? OccupationalLevelId { get; set; }
    public int? ReportToId { get; set; }
    public int? BranchId { get; set; }
    public int? DepartmentId { get; set; }
    public int? ProvinceId { get; set; }
    public int? SupportFunctionId { get; set; }
    public int? OccupationalStatusId { get; set; }
    public int? AppointmentTypeId { get; set; }
    public DateTime? EffectiveDate { get; set; }
}

public class EmployeeTransferDisplayDataModel
{
    public long EmployeeTransferId { get; set; }
    public string EmployeeCode { get; set; } = "";
    public string EmployeeInitialsSurname { get; set; } = "";
    public string JobTitle { get; set; } = "";
    public string JobTitleCode { get; set; } = "";
    public string JobGrade { get; set; } = "";
    public string WspCategory { get; set; } = "";
    public string OfoCode { get; set; } = "";
    public string MajorCostCenter { get; set; } = "";
    public string NicGrade { get; set; } = "";
    public string OccupationalCategory { get; set; } = "";
    public string OccupationalLevel { get; set; } = "";
    public string EffectiveDate { get; set; } = "";
    public string ReportTo { get; set; } = "";
    public string Branch { get; set; } = "";
    public string Department { get; set; } = "";
    public string Province { get; set; } = "";
    public string CoreSupportFunction { get; set; } = "";
    public string? OccupationalStatus { get; set; }
    public string? AppointmentType{ get; set; }
}