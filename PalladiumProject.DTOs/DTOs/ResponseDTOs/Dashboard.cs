using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs
{
    public class Dashboard
    {
        public int TotalEmployee { get; set; }
        public int ActiveEmployee { get; set; }
        public int TerminatedEmployee { get; set; }
        public int ProcessedEmployee { get; set; }
        public int UnprocessedEmployee { get; set; }
        public decimal Earnings { get; set; }
        public decimal Deduction { get; set; }
        public decimal CompanyContribution { get; set; }
        public decimal FringeBenefit { get; set; }
        public decimal EarningsPercentage { get; set; }
        public decimal DeductionPercentage { get; set; }
        public decimal CompanyContributionPercentage { get; set; }
        public decimal FringeBenefitPercentage { get; set; }
        public List<EmployeeBirhday> BirthdayList { get; set; } = new();
        public List<PayrollCycles> PayrollCycleList { get; set; } = new();
    }
    public class EmployeeBirhday
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime DateOfBirth {  get; set; } 
    }
    public class PayrollCycles
    {
        public int PayrollSetupId { get; set; }
        public string PayrollCycleName { get; set; } = string.Empty;
        public string CycleEndDate { get; set; } = string.Empty;
        public int TotalEmployees { get; set; }
    }
}
