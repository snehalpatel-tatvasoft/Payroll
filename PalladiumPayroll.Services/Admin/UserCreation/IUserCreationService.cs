using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Admin;

namespace PalladiumPayroll.Services.Admin
{
    public interface IUserCreationService
    {
        Task<JsonResult> CreateUser(UserCreationRequestDTO request);
        Task<JsonResult> UpdateUser(UserCreationRequestDTO request, Guid id);
        Task<JsonResult> DeleteUser(Guid id, long companyId);
        Task<JsonResult> GetUsersByCompanyId(long companyId);
        Task<JsonResult> GetAccessRolesNamesByCompanyId(long companyId);
        Task<JsonResult> GetUserById(Guid id, long companyId);
        Task<JsonResult> GetUserFunctionalityById(Guid userId);
        Task<JsonResult> SaveUserFunctionalityAccessRights(List<SaveUserFunctionalityAccessRightsDTO> request);
        Task<JsonResult> GetUserPayFrequenciesById(Guid userId, long companyId);
        Task<JsonResult> SaveUserPayFrequenciesAccessRights(List<SaveUserPayFrequencyAccessRightsDTO> request);
        Task<JsonResult> GetUserFunctionalityByIdForTransactionFunctions(Guid userId, long companyId);
    }
}