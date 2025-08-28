using PalladiumPayroll.DTOs.DTOs.Common;

namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave
{
    public class EmployeeLeaveFilterViewModel : TableFilterViewModel
    {
        public int CompanyId { get; set; }
        public DateTime? SatrtDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LeaveTyepId { get; set; }
    }
}
