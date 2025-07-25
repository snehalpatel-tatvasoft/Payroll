using PalladiumPayroll.DTOs.DTOs.Common;

namespace PalladiumPayroll.DTOs.DTOs.Employees
{
    public class EmployeeFilterViewModel : TableFilterViewModel
    {
        public int CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public bool? IsActive { get; set; } = true;
    }

    public class EmployeePaymentDetail
    {
        public int? EmployeeId { get; set; }
        public int PaymentMethod { get; set; }
        public string? AccountHolderName { get; set; }
        public string? AccountNumber { get; set; }
        public int? TypeofAccount { get; set; }
        public int? BankId { get; set; }
        public int? BranchCode { get; set; }
        public string? AccountHolderRelation { get; set; }
        public bool? SplitPayment { get; set; }
        public string? AccountHolderName1 { get; set; }
        public string? AccountNumber1 { get; set; }
        public int? TypeofAccount1 { get; set; }
        public int? BankId1 { get; set; }
        public int? BranchCode1 { get; set; }
        public string? AccountHolderRelation1 { get; set; }
        public decimal? SplitAmount1 { get; set; }
        public decimal? SplitAmount2 { get; set; }
        public decimal? SplitPercent1 { get; set; }
        public decimal? SplitPercent2 { get; set; }
    }
}
