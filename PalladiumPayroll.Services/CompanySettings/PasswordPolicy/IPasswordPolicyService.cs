using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

namespace PalladiumPayroll.Services.CompanySettings
{
    public interface IPasswordPolicyService
    {
        Task<JsonResult> GetPasswordPolicyByCompanyId(long companyId);
        Task<JsonResult> CreatePasswordPolicy(PasswordPolicyRequestDTO request);
        Task<JsonResult> UpdatePasswordPolicy(PasswordPolicyRequestDTO request, long passwordPolicyId);
        Task<JsonResult> DeletePasswordPolicy(long passwordPolicyId, long companyId);
    }
}