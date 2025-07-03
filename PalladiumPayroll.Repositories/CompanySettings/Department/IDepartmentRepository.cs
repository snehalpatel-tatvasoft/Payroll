// using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

// namespace PalladiumPayroll.Repositories.Department
// {
//     public interface IDepartmentRepository
//     {
//         Task<List<DepartmentDto>> GetDepartmentsByCompanyId(long companyId);
//         Task<long> CreateDepartment(CreateDepartmentRequestDto request);
//     }
// }

using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Repositories.Department
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDto>> GetDepartmentsByCompanyId(long companyId);
        Task<long> CreateDepartment(CreateDepartmentRequestDto request);
        Task<bool> EditDepartment(long departmentId, EditDepartmentRequestDto request);
        Task<bool> DeleteDepartment(long departmentId);
        Task<bool> CheckDepartmentNameExists(long companyId, string departmentName);
    }
}