using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Admin;

namespace PalladiumPayroll.Repositories.Admin
{
    public interface IUserCreationRepository
    {
        Task<int> CreateUser(UserCreationRequestDTO request);
        Task<int> UpdateUser(UserCreationRequestDTO request, Guid id);
        Task<int> DeleteUser(Guid id, long companyId);
        Task<List<UserListResponseDTO>> GetUsersByCompanyId(long companyId);
    }
}