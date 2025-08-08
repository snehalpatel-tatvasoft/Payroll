using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.CompanySettings
{
    public class PasswordPolicyService : IPasswordPolicyService
    {
        private readonly IPasswordPolicyRepository _passwordPolicyRepository;

        public PasswordPolicyService(IPasswordPolicyRepository passwordPolicyRepository)
        {
            _passwordPolicyRepository = passwordPolicyRepository;
        }

        public async Task<JsonResult> GetPasswordPolicyByCompanyId(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var policies = await _passwordPolicyRepository.GetPasswordPolicyByCompanyId(companyId);
                if (policies.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(policies, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Password Policies");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching password policies: {ex.Message}");
            }
        }

        public async Task<JsonResult> CreatePasswordPolicy(PasswordPolicyRequestDTO request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.PolicyName) || request.CompanyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                if (request.MinLength < 0 || request.MaxLength < request.MinLength ||
                    request.NoOfUppercaseLetters < 0 || request.NoOfDigits < 0 ||
                    request.NoOfSpecialLetters < 0 || request.PasswordAgeInterval <= 0 ||
                    request.SessionTimeOutInterval <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var passwordPolicyId = await _passwordPolicyRepository.CreatePasswordPolicy(request);
                if (passwordPolicyId == -1)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Password policy with this name already exists for the company.");
                }

                return HttpStatusCodeResponse.SuccessResponse(passwordPolicyId, "Password policy created successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error creating password policy: {ex.Message}");
            }
        }

        public async Task<JsonResult> UpdatePasswordPolicy(PasswordPolicyRequestDTO request, long passwordPolicyId)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.PolicyName) || request.CompanyId <= 0 || passwordPolicyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                if (request.MinLength < 0 || request.MaxLength < request.MinLength ||
                    request.NoOfUppercaseLetters < 0 || request.NoOfDigits < 0 ||
                    request.NoOfSpecialLetters < 0 || request.PasswordAgeInterval <= 0 ||
                    request.SessionTimeOutInterval <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var result = await _passwordPolicyRepository.UpdatePasswordPolicy(request, passwordPolicyId);
                if (result == -1)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Password policy with this name already exists for the company.");
                }
                if (result == -2)
                {
                    return HttpStatusCodeResponse.NotFoundResponse("Password policy not found.");
                }

                return HttpStatusCodeResponse.SuccessResponse(result, "Password policy updated successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error updating password policy: {ex.Message}");
            }
        }
        
        public async Task<JsonResult> DeletePasswordPolicy(long passwordPolicyId, long companyId)
        {
            try
            {
                if (passwordPolicyId <= 0 || companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var result = await _passwordPolicyRepository.DeletePasswordPolicy(passwordPolicyId, companyId);
                if (result == -2)
                {
                    return HttpStatusCodeResponse.NotFoundResponse("Password policy not found.");
                }

                return HttpStatusCodeResponse.SuccessResponse(result, "Password policy deleted successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error deleting password policy: {ex.Message}");
            }
        }
    }
}