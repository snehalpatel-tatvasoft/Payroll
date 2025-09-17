using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

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
            List<PasswordPolicyResponseDTO>? policies = await _passwordPolicyRepository.GetPasswordPolicyByCompanyId(companyId);

            return HttpStatusCodeResponse.SuccessResponse(policies, string.Format(ResponseMessages.Success, ResponseMessages.PasswordPolicy, ActionType.Retrieved));
        }

        public async Task<JsonResult> CreatePasswordPolicy(PasswordPolicyRequestDTO request)
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

            long passwordPolicyId = await _passwordPolicyRepository.CreatePasswordPolicy(request);
            if (passwordPolicyId == -1)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(
                    ResponseMessages.AlreadyExist,
                    ResponseMessages.PasswordPolicy + " with this name"
                ));
            }

            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.PasswordPolicy, ActionType.Created));
        }

        public async Task<JsonResult> UpdatePasswordPolicy(PasswordPolicyRequestDTO request, long passwordPolicyId)
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

            long result = await _passwordPolicyRepository.UpdatePasswordPolicy(request, passwordPolicyId);
            
            if (result == -1)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(
                    ResponseMessages.AlreadyExist,
                    ResponseMessages.PasswordPolicy + " with this name"
                ));
            }
            if (result == -2)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.PasswordNotFound);
            }

            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.PasswordPolicy, ActionType.Updated));
        }

        public async Task<JsonResult> DeletePasswordPolicy(long passwordPolicyId, long companyId)
        {
            long result = await _passwordPolicyRepository.DeletePasswordPolicy(passwordPolicyId, companyId);
            if (result == -2)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.PasswordNotFound);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.PasswordPolicy, ActionType.Deleted));
        }
    }
}