namespace PalladiumPayroll.DTOs.DTOs
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeSurname { get; set; }
    }

    public class EmployeeList
    {
        public List<Employee>? Employees { get; set; }
        public int? EmployeeCount { get; set; }
    }
}
