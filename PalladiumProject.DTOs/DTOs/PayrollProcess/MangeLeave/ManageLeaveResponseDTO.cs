namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave
{
    public class EmployeeLeaveViewModel
    {
        public int EmployeeLeaveDetailId { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public int LeaveType { get; set; }
        public int LeaveStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Duration { get; set; }
        public int TotalCount { get; set; }
    }
    
    public class BatchData
    {
        public int BatchId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Status { get; set; }
        public string PayrollCycleName { get; set; } = null!;
        public string ProcessPeriod { get; set; } = null!;
    }

    public class BatchLeaveImportData
    {
        public int BatchId { get; set; }
        public int LeaveDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal DueDays { get; set; }
        public decimal? TotalDays { get; set; }
        public string? Comment { get; set; }
    }

    public class BatchLeaveImportActualData : BatchLeaveImportData
    {
        public string BatchNumber { get; set; }
        public string BatchDescription { get; set; }
        public int PayrollCycle { get; set; }
        public int ProcessPeriod { get; set; }
        public decimal? DaysPaid { get; set; }
        public decimal? DaysUnpaid { get; set; }
    }


    public class BatchLeaveAttachment
    {
        public int DocumentLeaveId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentUrl { get; set; }
    }

    public class UnApprovedLeaves
    {
        public int EmployeeLeaveDetailId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string ApproverName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RequestedDate { get; set; }
        public decimal Duration { get; set; }
    }

    public class LeaveHistory
    {
        public string BatchName { get; set; }
        public int LeaveId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Duration { get; set; }
        public string? DocumnetUrl { get; set; }
        public int LeaveStatusTypeId { get; set; }
    }

    public class EmployeeDueDays
    {
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int DaysDue { get; set; }
    }
}
