namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave
{
    public class EmployeeLeaveViewModel
    {
        public int EmployeeLeaveDetailId { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public int LeaveTypeId { get; set; }
        public DateTime SatrtDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
    }
}
