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