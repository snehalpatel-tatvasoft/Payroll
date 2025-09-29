namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip
{
    public class BatchPayslipInsert
    {
        public int? BatchId { get; set; }
        public bool? Mode { get; set; } = false;
        public string BatchName { get; set; }
        public string? BatchDescription { get; set; }
        public int CompanyId { get; set; }
        public int CycleId { get; set; }
        public int ProcessPriod { get; set; }
        public bool? IsRecurring { get; set; }
        public bool? IsSpecialRun { get; set; }
        public bool? IsLeavePay { get; set; }
    }
}
