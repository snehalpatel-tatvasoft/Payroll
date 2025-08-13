namespace PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

public class DataImportRequestDTO
{
}

public class PayrollTransactionFilterViewModel
{
    public long CompanyId { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "PayrollProcessId";
    public bool SortType { get; set; } = true;
    public string Search { get; set; } = "";
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

public class ImportYTDRecordRequestDTO
{
    public int CreatedBy { get; set; } = 1;
    public List<YearToDateRecordDTO> YearToDateRecords { get; set; } = new List<YearToDateRecordDTO>();
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


public class ImportWorkInformationRequestDTO
{
    public string UserId { get; set; } = "";
    public List<WorkInformationDTO> WorkInformations { get; set; } = new List<WorkInformationDTO>();
}