using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Repositories.Department
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentResponseDTO>> GetDepartmentsByCompanyId(long companyId);
        Task<long> CreateDepartment(DepartmentRequestDTO request);
        Task<bool> EditDepartment(long departmentId, DepartmentRequestDTO request);
        Task<bool> DeleteDepartment(long departmentId);
        Task<bool> CheckDepartmentNameExists(long companyId, string departmentName);
    }
}