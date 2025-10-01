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
        public int CycleId { get; set; }
        public int ProcessPriod { get; set; }
        public bool? IsRecurring { get; set; }
    }

    public class MultiTransactionGet
    {
        public int ProcessPreiodId { get; set; }
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
        public decimal TransactionValues { get; set; } = 0.00M;
        public int CompanyId { get; set; }
    }

    public class BatchPayslipBulkInsert : BatchPayslipInsert
    {
        public List<int> EmployeeIds { get; set; }
        public List<BatchTransactionTbl> BatchTransaction { get; set; }
    }

    public class BatchTransactionTbl
    {
	    public string TransactionName { get; set; }
        public decimal Amount { get; set; }
        public decimal Hours { get; set; }
    }
}
