namespace PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;

public class EmployeeTrainingDisplayDataDTO
{
    public long EmployeeTrainingId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;         
    public string Course { get; set; } = string.Empty;
    public string CourseType { get; set; } = string.Empty;
    public string CourseStatus { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
}


public class EmployeeTrainingDetailDTO
{
    public long EmployeeTrainingId { get; set; }
    public long CompanyId { get; set; }
    public long EmployeeId { get; set; }
    public DateTime? TrainingDate { get; set; }
    public int? CourseId { get; set; }
    public int? Duration { get; set; }
    public int? DurationId { get; set; }
    public decimal? ActualCost { get; set; }
    public decimal? BudgetCost { get; set; }
    public int? CourseTypeId { get; set; }
    public int? InstitutionId { get; set; }
    public string? CertificateNumber { get; set; }
    public int? ResultId { get; set; }
    public int? CourseStatusId { get; set; }
    public string? TrainerDetails { get; set; }
    public int? NQFLevelId { get; set; }
    public int? UnitStandardId { get; set; }
    public bool? SAQARequired { get; set; }
    public string? Comments { get; set; }
    public string? FilePath { get; set; }
}


public class CourseDto
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
}

public class CourseTypeDto
{
    public int CourseTypeId { get; set; }
    public string CourseTypeName { get; set; } = string.Empty;
}

public class InstitutionDto
{
    public int InstitutionId { get; set; }
    public string InstitutionName { get; set; } = string.Empty;
}

public class NQFLevelDto
{
    public int NQFLevelId { get; set; }
    public string NQFLevelName { get; set; } = string.Empty;
}

public class UnitStandardDto
{
    public int UnitStandardId { get; set; }
    public string UnitStandardName { get; set; } = string.Empty;
}

public class DurationDto
{
    public int DurationId { get; set; }
    public string DurationName { get; set; } = string.Empty;
}

public class ResultDto
{
    public int ResultId { get; set; }
    public string ResultName { get; set; } = string.Empty;
}

public class CourseStatusDto
{
    public int CourseStatusId { get; set; }
    public string CourseStatusName { get; set; } = string.Empty;
}

public class EmployeeDto
{
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeeSurname { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
}

public class EmployeeTrainingDropdownsDTO
{
    public List<CourseDto> Courses { get; set; } = new();
    public List<CourseTypeDto> CourseTypes { get; set; } = new();
    public List<InstitutionDto> Institutions { get; set; } = new();
    public List<NQFLevelDto> NQFLevels { get; set; } = new();
    public List<UnitStandardDto> UnitStandards { get; set; } = new();
    public List<DurationDto> Durations { get; set; } = new();
    public List<ResultDto> Results { get; set; } = new();
    public List<CourseStatusDto> CourseStatuses { get; set; } = new();
    public List<EmployeeDto> Employees { get; set; } = new();
}

