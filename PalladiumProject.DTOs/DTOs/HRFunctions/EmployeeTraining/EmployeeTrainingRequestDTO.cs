using Microsoft.AspNetCore.Http;

namespace PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;

public class EmployeeTrainingUpsertData
{
    public long? EmployeeTrainingId { get; set; }
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
    public string? FileName { get; set; }
    public string? FileType { get; set; }
    public long? FileSize { get; set; }
    public string UserId { get; set; } = null!;
    public IFormFile? File { get; set; }
}


public class EmployeeTrainingDropdownItem
{
    public string Name { get; set; } = "";
    public long CompanyId { get; set; }
    public int Type { get; set; }
}


public class TrainingDocumentDelete
{
    public string DocumentUrl { get; set; }="";
}