namespace PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

public class PayrollTransactionFilterViewModel
{
    public long CompanyId { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "PayrollProcessId";
    public bool SortType { get; set; } = true;
    public string Search { get; set; } = "";
}

public class EmployeeMasterImport
{
    public string? EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public string? EmployeeSurname { get; set; }
    public string? Initials { get; set; }
    public int? Gender { get; set; }
    public string? PreferredName { get; set; }
    public string? IDNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PassportNumber { get; set; }
    public int? PassportIssuedBy { get; set; }
    public bool? IsAsylumSeeker { get; set; }
    public string? AsylumPermitNumber { get; set; }
    public bool? IsRefugee { get; set; }
    public string? UnitNumber { get; set; }
    public string? Phy_CountryId { get; set; }
    public string? ComplexName { get; set; }
    public string? StreetNumber { get; set; }
    public string? StreetName { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? Phy_PostalCode { get; set; }
    public bool? IsPostalSame { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? Pos_PostalCode { get; set; }
    public string? HomeNumber { get; set; }
    public string? CellNumber { get; set; }
    public string? Email { get; set; }
    public DateTime? StartDate { get; set; }
    public string? IncomeTaxNumber { get; set; }
    public long? NatureofPersonId { get; set; }
    public string? DesignationId { get; set; }
    public string? PayrollSetupId { get; set; }
    public bool? FlagEndEmployment { get; set; }
    public DateTime? EndEmploymentDate { get; set; }
    public string? ProfileId { get; set; }
    public int? EmploymentStatus { get; set; }
    public string? PaymentTypeId { get; set; }
    public string? Bank1Id { get; set; }
    public string? AccountTypeId { get; set; }
    public string? AccountName { get; set; }
    public string? AccountNumber { get; set; }
    public string? Branch1Code { get; set; }
    public decimal? SplitAmount1 { get; set; }
    public decimal? SplitPercentage1 { get; set; }
    public bool? IsJointAccount { get; set; }
    public string? AccountHolderRelationship { get; set; }
    public string? Branch2Code { get; set; }
    public string? Bank2Id { get; set; }
    public string? AccountName1 { get; set; }
    public string? AccountNumber1 { get; set; }
    public string? AccountTypeId1 { get; set; }
    public decimal? SplitAmount2 { get; set; }
    public decimal? SplitPercentage2 { get; set; }
    public bool? IsJointAccount1 { get; set; }
    public string? AccountHolderRelationship1 { get; set; }
    public string? Name { get; set; }
    public string? Relationship { get; set; }
    public string? Phone { get; set; }
    public string? DepartmentId { get; set; }
    public string? RaceId { get; set; }
    public int? Title { get; set; }
    public bool? CopyCompanyAddress { get; set; }
}

public class EmployeeMasterImportRequestDTO
{
    public long CompanyId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public string ImportFileName { get; set; } = string.Empty;
    public List<EmployeeMasterImport> Data { get; set; } = new();
}

public class ImportYearToDateTemplateDto
{
    public long CompanyId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public string TransactionList { get; set; } = string.Empty;
}


public class YearToDateRecordDTO
{
    public string EmployeeCode { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
}

public class ImportYearToDateRecordRequestDTO
{
    public List<YearToDateRecordDTO> Records { get; set; } = new();
    public string TemplateName { get; set; } = string.Empty;
    public string ImportFileName { get; set; } = string.Empty;
    public long CompanyId { get; set; }
}


public class WorkInformationDTO
{
    public string EmployeeCode { get; set; } = "";
    public decimal AnnualSalary { get; set; }
    public decimal MonthlySalary { get; set; }
    public decimal RatePerDay { get; set; }
    public decimal RatePerHour { get; set; }
    public decimal DaysPerWeek { get; set; }
    public decimal HoursPerWeek { get; set; }
    public decimal HoursPerDay { get; set; }
    public string StandardWorkingDays { get; set; } = "";
}

public class WorkInformationImportRequestDTO
{
    public List<WorkInformationDTO> Records { get; set; } = new();
    public string TemplateName { get; set; } = string.Empty;
    public string ImportFileName { get; set; } = string.Empty;
    public long CompanyId { get; set; }
}

public class ImportStatusFilterViewModel
{
    public long CompanyId { get; set; }
    public string? TemplateName { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public bool SortType { get; set; } = false; 
}
