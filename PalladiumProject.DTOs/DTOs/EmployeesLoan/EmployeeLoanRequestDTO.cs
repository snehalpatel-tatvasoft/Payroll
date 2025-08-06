namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.EmployeesLoan;

public class EmployeeLoanRequestDTO
{
    public long? EmployeeLoanId { get; set; }
    public int EmployeeId { get; set; } 
    public string? EmployeeCode { get; set; }
    public DateTime? RepaymentStartDate { get; set; }
    public DateTime? LoanGrantedDate { get; set; }
    public long? NumberOfRepayment { get; set; }
    public decimal? CurrentrePaymentAmount { get; set; }
    public decimal? LoanAmount { get; set; }
    public decimal? LoanMaxdeductionInterestRate { get; set; }
    public decimal? InterestRate { get; set; }
    public decimal? ActualLoanAmount { get; set; }
    public string? LoanIntegration { get; set; } = string.Empty;
    public int? UserId { get; set; }

}
