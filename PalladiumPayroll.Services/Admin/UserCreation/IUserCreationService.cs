using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;

namespace PalladiumPayroll.Services.Admin
{
    public interface IUserCreationService
    {
        Task<JsonResult> CreateUser(UserCreationRequestDTO request);
        Task<JsonResult> UpdateUser(UserCreationRequestDTO request, Guid id);
        Task<JsonResult> DeleteUser(Guid id, long companyId);
        Task<JsonResult> GetUsersByCompanyId(long companyId);
    }
}