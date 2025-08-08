namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings
{
   public class TimesheetSetupRequestDTO
    {
        public long CompanyId { get; set; }
        public bool IsClockCard { get; set; }
        public List<long> CompanyPayrollIds { get; set; } = new List<long>();
    }

}