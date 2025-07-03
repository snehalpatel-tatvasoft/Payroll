namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs
{
    public class DepartmentDto
    {
        public long DepartmentId { get; set; }
        public long? CompanyId { get; set; }
        public string? DepartmentName { get; set; }
    }

    public class CreateDepartmentRequestDto
    {
        public long CompanyId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EditDepartmentRequestDto
    {
        public string DepartmentName { get; set; }
    }
}