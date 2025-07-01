using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs
{
    public class PayrollSummaryResponse
    {
        public decimal Earnings { get; set; }
        public decimal Deduction { get; set; }
        public decimal CompanyContribution { get; set; }
        public decimal FringeBenefit { get; set; }
        public decimal EarningsPercentage { get; set; }
        public decimal DeductionPercentage { get; set; }
        public decimal CompanyContributionPercentage { get; set; }
        public decimal FringeBenefitPercentage { get; set; }
        public decimal TotalRemuneration {  get; set; }
    }
}
