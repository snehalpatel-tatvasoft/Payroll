using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.User;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserResponse>> GetUserInfo(string email)
        {
            return await _userRepository.GetUserInfo(email);
        }

        public async Task<bool> ConfirmEmail(string userId)
        {
            return await _userRepository.ConfirmEmail(userId);
        }

        public async Task<bool> CheckIsUserLoggedIn(string userId)
        {
            return await _userRepository.CheckIsUserLoggedIn(userId);
        }

        public async Task<bool> UpdateLastActivity(string userId)
        {
            return await _userRepository.UpdateLastActivity(userId);
        }

        public async Task<bool> LogoutInactiveUsers()
        {
            return await _userRepository.LogoutInactiveUsers();
        }

        public async Task<List<CompanyDetails>> GetCompaniesByEmail(string email)
        {
            return await _userRepository.GetCompaniesByEmail(email);
        }

        public async Task<JsonResult> ChangeEmail(ChangeEmailModel reqModel)
        {
            var res = await _userRepository.UpdateUserEmail(reqModel.Email, reqModel.NewEmail);
            if(res == -1)
            {
                return HttpStatusCodeResponse.SuccessResponse("OldMisMatch", "email address is invalid !");
            }
            else if(res == 1)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Email", ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InternalServerError);
        }

        public async Task<JsonResult> ChangePassword(ChangePasswordModel reqModel)
        {
            string? oldPassword = await _userRepository.GetUserPassword();
            PasswordHasher<object>? hasher = new PasswordHasher<object>();
            var verificationResult = hasher.VerifyHashedPassword(string.Empty, oldPassword, reqModel.Password);
            if (verificationResult == PasswordVerificationResult.Success)
            {
                var res = await _userRepository.UpdateUserPassword(reqModel.NewPassword);
                if (res)
                {
                    return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.PasswordChanged);
                }
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InternalServerError);
            }
            return HttpStatusCodeResponse.SuccessResponse("OldMisMatch" ,ResponseMessages.LoginPasswordMismatch);
        }
    }
}
