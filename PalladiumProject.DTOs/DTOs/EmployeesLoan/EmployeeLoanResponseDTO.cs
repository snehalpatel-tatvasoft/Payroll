public class EmployeeLoanResponseDTO
{
    public long EmployeeLoanId { get; set; }
    public long EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime RepaymentStartDate { get; set; }
    public DateTime LoanGrantedDate { get; set; }
    public long NumberOfRepayment { get; set; }
    public decimal CurrentRepaymentAmount { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal LoanPaidAmount { get; set; }
    public decimal OutstandingAmount { get; set; }
    public string LoanStatusName { get; set; } = string.Empty;
    public decimal LoanMaxdeductionInterestRate { get; set; }
    public decimal InterestRate { get; set; }
    public decimal ActualLoanAmount { get; set; }
    public string LoanIntegration { get; set; } = string.Empty;
    public bool IsPaushed { get; set; }

}

public class EmployeeLoanDropdownsDTO
{
    public List<EmployeeLoanDataDropdownDto> Employees { get; set; } = new();
}
public class EmployeeLoanDataDropdownDto
{
    public long EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeeSurname { get; set; } = string.Empty;
}
