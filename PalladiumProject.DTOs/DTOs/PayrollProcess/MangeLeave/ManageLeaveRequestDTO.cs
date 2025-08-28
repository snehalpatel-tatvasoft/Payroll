using PalladiumPayroll.DTOs.DTOs.Common;

namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave
{
    public class EmployeeLeaveFilterViewModel : TableFilterViewModel
    {
        public int CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EmployeeId { get; set; }
        public int? LeaveType { get; set; }
        public int? LeaveStatus { get; set; }
    }

    public class AddEmployeeLeaves
    {
        public int LeaveDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveType { get; set; }
        public int LeaveStatus { get; set; }
        public DateTime FromDate { get; set; }
        public int FromTime { get; set; }
        public DateTime ToDate { get; set; }
        public int ToTime { get; set; }
        public decimal Duration { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Comment { get; set; }
    }
}
