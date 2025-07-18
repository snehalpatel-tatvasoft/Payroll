namespace PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeePromotions;

public class JobGradeDto
{
    public long JobGradeId { get; set; }
    public string JobGradeName { get; set; }="";
}

public class WSPCategoryDto
{
    public long WSPCategoryId { get; set; }
    public string WSPCategoryName { get; set; }="";
}

public class OFOCodeDto
{
    public long OFOCodeId { get; set; }
    public string OFOCodeName { get; set; }="";
}

public class MajorCostCenterDto
{
    public long MajorCostCenterId { get; set; }
    public string MajorCostCenterName { get; set; }="";
}

public class NICGradeDto
{
    public long NICGradeId { get; set; }
    public string NICGradeName { get; set; }="";
}

public class OccupationalCategoryDto
{
    public long OccupationalCategoryId { get; set; }
    public string OccupationalCategoryName { get; set; }="";
}

public class OccupationalLevelDto
{
    public long OccupationalLevelId { get; set; }
    public string OccupationalLevelName { get; set; }="";
}

public class BranchDto
{
    public long BranchId { get; set; }
    public string BranchName { get; set; }="";
}

public class ProvinceDto
{
    public long ProvinceId { get; set; }
    public string ProvinceName { get; set; }="";
}

public class SupportFunctionDto
{
    public long SupportFunctionId { get; set; }
    public string SupportFunctionName { get; set; }="";
}

public class DepartmentDto
{
    public long DepartmentId { get; set; }
    public string DepartmentName { get; set; }="";
}

public class EmployeeDropdownDto
{
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; }="";
    public string EmployeeSurname { get; set; }="";
    public string EmployeeCode { get; set; }="";
    public string Initials { get; set; }="";
}

public class DesignationCodeDto
{
    public string DesignationCode { get; set; }="";
}


public class EmployeePromotionDropdownsDTO
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
    public List<EmployeeDropdownDto> Employees { get; set; } = new();
    public List<DesignationCodeDto> DesignationCodes { get; set; } = new();
}

public class EmployeePromotionsdisplayDataDTO
{
}

public class EmployeePromotionDetailDTO
{
}
