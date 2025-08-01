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
    public class TransactionType
    {
        public long PayrollProcessId { get; set; }
        public string Description { get; set; } = "";
    }

    public class TransactionTypeDropdownsDTO
    {
        public List<TransactionType> TransactionType { get; set; } = new();
    }
    public class GetDirectiveResponse
    {
        public int Id { get; set; }
        public string DirectiveNumber { get; set; } = string.Empty;
        public DateTime DirectiveDate { get; set; }
        public int SourceCode { get; set; } 
        public decimal DirectiveAmount { get; set; }
        public string TypeIndicator { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public bool Status { get; set; }
    }


}
