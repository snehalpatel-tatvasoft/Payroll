namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs
{
    public class DepartmentResponseDTO
    {
        public long DepartmentId { get; set; }
        public long? CompanyId { get; set; }
        public string? DepartmentName { get; set; }
    }

    public class AddEditDepartmentRequestDTO
    {
        public long CompanyId { get; set; }
        public string DepartmentName { get; set; }
    }

}