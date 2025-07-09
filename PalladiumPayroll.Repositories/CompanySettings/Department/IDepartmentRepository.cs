using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Repositories.Department
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentResponseDTO>> GetDepartmentsByCompanyId(long companyId);
        Task<long> CreateDepartment(AddEditDepartmentRequestDTO request);
        Task<bool> EditDepartment(long departmentId, AddEditDepartmentRequestDTO request);
        Task<bool> DeleteDepartment(long departmentId);
        Task<bool> CheckDepartmentNameExists(long companyId, string departmentName);
    }
}