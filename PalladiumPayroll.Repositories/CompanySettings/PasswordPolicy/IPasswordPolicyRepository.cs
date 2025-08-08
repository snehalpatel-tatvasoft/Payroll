using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Repositories.CompanySettings
{
    public interface IPasswordPolicyRepository
    {
        Task<List<PasswordPolicyResponseDTO>> GetPasswordPolicyByCompanyId(long companyId);
        Task<long> CreatePasswordPolicy(PasswordPolicyRequestDTO request);
        Task<long> UpdatePasswordPolicy(PasswordPolicyRequestDTO request, long passwordPolicyId);
        Task<long> DeletePasswordPolicy(long passwordPolicyId, long companyId);
    }
}