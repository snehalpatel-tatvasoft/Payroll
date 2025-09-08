using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using System.ServiceModel.Channels;

namespace PalladiumPayroll.Repositories.Department
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentResponseDTO>> GetDepartmentsByCompanyId(long companyId);
        Task<long> CreateDepartment(DepartmentRequestDTO request);
        Task<bool> EditDepartment(long departmentId, DepartmentRequestDTO request);
        Task<(bool isSuccess, string message)> DeleteDepartment(long departmentId,long? employeeId);
        Task<bool> CheckDepartmentNameExists(long companyId, string departmentName);
    }
}