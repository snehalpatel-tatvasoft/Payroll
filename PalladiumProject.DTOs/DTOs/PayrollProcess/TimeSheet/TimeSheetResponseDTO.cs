namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.TimeSheet
{
    public class TimeSheetImport
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
    }
}
