using Microsoft.AspNetCore.Http;

namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip
{
    public class BatchPayslipInsert
    {
        public int? BatchId { get; set; }
        public bool? Mode { get; set; } = false;
        public string BatchNumber { get; set; }
        public string? BatchDescription { get; set; }
        public int TransactionType { get; set; }
        public int CompanyId { get; set; }
        public int PayrollCycle { get; set; }
        public int ProcessPeriod { get; set; }
        public bool? IsRecurring { get; set; }
    }

    public class MultiTransactionGet
    {
        public int ProcessPeriod { get; set; }
        public int CompanyId { get; set; }
        public int TransactionType { get; set; }
    }

    public class BatchTransactionUpdate
    {
        public int BatchTransactionId { get; set; }
        public int EmployeeId { get; set; }
        public int TransactionType { get; set; }
        public string? TransactionName { get; set; }
        public decimal Unit { get; set; } = 0.00M;
        public bool? IsRecurring { get; set; } = false;
        public decimal TransactionValues { get; set; } = 0.00M;
        public int CompanyId { get; set; }
    }

    public class BatchPayslipBulkInsert : BatchPayslipInsert
    {
        public List<int> EmployeeIds { get; set; } = null!;
        public List<BatchTransactionTbl> BatchTransaction { get; set; } = null!;
    }

    public class ImportBatchPayslipBulkInsert
    {
        public IFormFile BatchTransaction { get; set; } = null!;
        public BatchPayslipInsert BatchPayslipInfo { get; set; } = null!;
    }

    public class BatchTransactionTbl
    {
	    public string TransactionName { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal Hours { get; set; }
        public bool? IsRecurring { get; set; }
    }

    public class PayslipLeave
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Duration { get; set; }
        public decimal? UnPaidLeave { get; set; }
        public int? LeaveStatusId { get; set; }
        public string? Comment { get; set; }
    }

    public class BatchPayslipProcess
    {
        public int BatchId { get; set; }
        public long CompanyId { get; set; }
        public bool IsAppend { get; set; }
        public int TransactionType { get; set; }

    }
}
