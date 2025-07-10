namespace PalladiumPayroll.DTOs.DTOs.Employees
{
    public class EmployeeDataViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string IDNumber { get; set; }
        public DateOnly Dob { get; set; }
        public string PayrollCycle { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
    }
}
