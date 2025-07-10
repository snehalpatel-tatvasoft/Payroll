namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings
{
    public class TimesheetSetupResponseDTO
    {
        public long CompanyPayrollId { get; set; }
        public string PayrollCycleName { get; set; } = null!;
    }

    public class TimesheetPayrollSetupResponseDTO
    {
        public long CompanyPayrollId { get; set; }
        public bool IsClockCard { get; set; }
    }

}