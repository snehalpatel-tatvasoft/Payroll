using System.Numerics;

namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip
{
    public class BatchPayslipTransaction
    {
        public int BatchTransactionId { get; set; }
        public int BatchId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string TransactionName { get; set; } = string.Empty;
        public int? PayrollTransactionId { get; set; }
        public BigInteger? TransactionOrder { get; set; }
        public decimal? TransactionValues { get; set; }
        public bool? IsUnit { get; set; } = false;
        public decimal? Rate { get; set; }
        public decimal? Unit { get; set; }
        public int? TransactionTypeId { get; set; }
        public bool? IsRecurring { get; set; } = false;
        public bool IsValid { get; set; } = false;
        public string? ValidateMessage { get; set; }
        public string? CountryCode { get; set; }
    }

    public class BatchPayslipLeave
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Duration { get; set; }
        public decimal AvailableDays { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public string? ValidationMessage { get; set; }
        public bool IsValid { get; set; }
        public string? WarningMessage { get; set; }
        public bool IsWarning { get; set; }
        public int LeaveBatchId { get; set; }
        public int EmployeeLeaveId { get; set; }
    }

    public class BatchFirstTransaction
    {
        public int BatchTransactionId { get; set; }
        public int BatchId { get; set; }
    }

    public class SpecialTransaction
    {
        public int PayrollProcessId { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public bool IsUnit { get; set; }
        public int CouncilOptionsId { get; set; }
    }
}
