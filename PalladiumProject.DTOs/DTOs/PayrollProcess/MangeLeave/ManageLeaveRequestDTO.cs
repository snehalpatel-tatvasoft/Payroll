using Microsoft.AspNetCore.Http;
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
        public string? Comment { get; set; }
    }

    public class BatchLeaveImport : BatchInfoRequest
    {
        public bool? IsActualLeave {  get; set; } = false;
        public IFormFile File { get; set; } = null!;
    }

    public class BatchInfoRequest
    {
        public int? BatchId { get; set; } = 0;
        public long CompanyId { get; set; }
        public int BatchNumber { get; set; }
        public string? BatchDescription { get; set; }
        public int PayrollCycle { get; set; }
        public int ProcessPeriod { get; set; }
    }

    public class BatchLeaveDetail : BatchInfoRequest
    {
        public long LeaveDetailId { get; set; }
        public bool? IsActualLeave { get; set; } = false;
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal DueDays { get; set; }
        public decimal? TotalDays { get; set; }
        public string Comment { get; set; }
    }

    public class AddBatchLeaveAttachment
    {
        public int LeaveDetailId { get; set; }
        public long EmployeeId { get; set; }
        public long BatchId { get; set; }
        public bool? IsActualLeave { get; set; } = false;
        public IFormFile File { get; set; } = null!;
    }

    public class BatchLeaveDocument
    {
        public int LeaveDetailId { get; set; }
        public long EmployeeId { get; set; }
        public long BatchId { get; set; }
        public bool? IsActualLeave { get; set; } = false;
        public string DocFileName { get; set; } = null!;
        public string DocFileUrl { get; set; } = null!;
    }
}
