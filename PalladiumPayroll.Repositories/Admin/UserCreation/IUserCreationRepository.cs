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
        Task<List<AccessRoleResponseDTO>> GetAccessRolesNamesByCompanyId(long companyId);
        Task<UserCreationRequestDTO> GetUserById(Guid id, long companyId);
        Task<List<UserFunctionalityAccessRightsDTO>> GetUserFunctionalityById(Guid userId);
        Task<bool> SaveUserFunctionalityAccessRights(List<SaveUserFunctionalityAccessRightsDTO> requests);
        Task<List<UserPayFrequencyAccessRightsDTO>> GetUserPayFrequenciesById(Guid userId, long companyId);
        Task<bool> SaveUserPayFrequenciesAccessRights(List<SaveUserPayFrequencyAccessRightsDTO> requests);
        Task<List<UserFunctionalityAccessRightsDTO>> GetUserFunctionalityByIdForTransactionFunctions(Guid userId, long companyId);
    }
}