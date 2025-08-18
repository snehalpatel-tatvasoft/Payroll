namespace PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

public class PayrollProcessingTransactionDto
{
    public long PayrollProcessId { get; set; }
    public string Description { get; set; }="";
    public long? Irp5Code { get; set; }
}

public class YTDTemplateDropdownDto
{
    public int TemplateId { get; set; }
    public string TemplateName { get; set; }="";
}

public class TransactionForExcelGenerateDto
{
    public long PayrollProcessId { get; set; }
    public string? Description { get; set; }
}

public class ImportStatusDto
{
    public long StatusId { get; set; }
    public string? TemplateName { get; set; }
    public string? ImportFileName { get; set; }
    public string? ErrorTableName { get; set; }
    public int IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
}
