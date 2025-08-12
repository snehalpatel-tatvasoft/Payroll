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
    public List<YearToDateRecordDTO> yearToDateRecords { get; set; } = new List<YearToDateRecordDTO>();
}