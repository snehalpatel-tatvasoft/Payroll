using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.DTOs.Miscellaneous.Constants;
using PalladiumPayroll.Helper;
using PalladiumPayroll.Helper.JWTToken;
using PalladiumPayroll.Repositories.User;
using PalladiumPayroll.Services;
using System.Net.Mail;
using System.Security.Claims;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings? _jwtSettings;
        private readonly PayrollWebSetting? _payrollWebSetting;
        private readonly EmailService _emailService;
        public AuthRepository(IConfiguration configuration, IUserRepository userRepository, EmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _jwtSettings = AppSettingsConfig.GetSection<JwtSettings>(configuration, "Jwt");
            _payrollWebSetting = AppSettingsConfig.GetSection<PayrollWebSetting>(configuration, "Payroll");
        }

        public async Task<JsonResult> Login(LoginRequest loginRequest)
        {
            // Fetch user by email
            List<UserResponse> userList = await _userRepository.GetUserInfo(loginRequest.Email);
            List<UserResponse> multiUserList = new List<UserResponse>();

            if (!userList.Any())
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UserNotFound);
            }
            else
            {
                foreach (var user in userList)
                {
                    PasswordHasher<object>? hasher = new PasswordHasher<object>();
                    var verificationResult = hasher.VerifyHashedPassword(string.Empty, user.PasswordHash, loginRequest.Password);

                    if (verificationResult == PasswordVerificationResult.Success)
                    {
                        multiUserList.Add(user);
                    }
                }

                if (multiUserList.Any())
                {
                    if (multiUserList.Count == 1)
                    {
                        var validUser = multiUserList[0];
                        if (!validUser.ConfirmedEmail)
                        {
                            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.AccountNotConfirmed);
                        }
                        var tokens = GetAccessTokenAndRefreshToken(validUser);
                        var result = new LoginResposeModel() { Token = tokens[0], RefreshToken = tokens[1], IsMultiUser = false, CompanyId = validUser.CompanyId };
                        await _userRepository.UpdateLastActivity(validUser.Id.ToString());
                        return HttpStatusCodeResponse.SuccessResponse(result, ResponseMessages.LoginSuccessfully);
                    }
                    else
                    {
                        var companies = multiUserList.Select(x => new CompanyDetails()
                        {
                            CompanyId = x.CompanyId,
                            CompanyName = x.CompanyName,
                            RoleId = x.RoleId,
                            UserId = x.Id,
                        }).ToList();
                        var result = new LoginResposeModel() { IsMultiUser = true, CompanyDetails = companies };
                        return HttpStatusCodeResponse.SuccessResponse(result, "Login Process");
                    }
                }
                else
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid credentials!");
                }
            }
        }

        public async Task<JsonResult> LoginSelectedUser(string userId)
        {
            var user = await _userRepository.GetUserInfoByUserId(userId);
            if (user != null)
            {
                if (!user.ConfirmedEmail)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.AccountNotConfirmed);
                }
                else
                {
                    var tokens = GetAccessTokenAndRefreshToken(user);
                    var result = new LoginResposeModel() { Token = tokens[0], RefreshToken = tokens[1], IsMultiUser = false, CompanyId = user.CompanyId };
                    await _userRepository.UpdateLastActivity(userId);
                    return HttpStatusCodeResponse.SuccessResponse(result, ResponseMessages.LoginSuccessfully);
                }
            }
            else
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UserNotFound);
            }
        }

        public async Task<JsonResult> ForgotPassWord(string email)
        {
            List<UserResponse> userList = await _userRepository.GetUserInfo(email);
            if (!userList.Any())
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UserNotFound);
            }
            else
            {
                if (userList.Count == 1)
                {
                    var singleUser = userList[0];
                    var isSent = await SendResetPasswordEmail(singleUser.Id, singleUser.Email, singleUser.UserName, singleUser.CompanyName);
                    if (isSent)
                    {
                        var result = new ForgotResponseModel() { IsMultiUser = false };
                        return HttpStatusCodeResponse.SuccessResponse(result, ResponseMessages.ResetPasswordEmailSentSuccesfully);
                    }
                    else
                    {
                        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmailSentFailure);
                    }
                }
                else
                {
                    var companies = userList.Select(x => new CompanyDetails()
                    {
                        CompanyId = x.CompanyId,
                        CompanyName = x.CompanyName,
                        RoleId = x.RoleId,
                        UserId = x.Id,
                    }).ToList();
                    var result = new ForgotResponseModel() { IsMultiUser = true, CompanyDetails = companies };
                    return HttpStatusCodeResponse.SuccessResponse(result, "Forgot Process");
                }
            }
        }

        public async Task<JsonResult> ForgotPasswordSelectedUser(string userId)
        {
            var user = await _userRepository.GetUserInfoByUserId(userId);
            if(user == null)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UserNotFound);
            }
            else
            {
                var isSent = await SendResetPasswordEmail(user.Id, user.Email, user.UserName, user.CompanyName);
                if (isSent)
                {
                    return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.ResetPasswordEmailSentSuccesfully);
                }
                else
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmailSentFailure);
                }

            }
        }

        public async Task<JsonResult> ResetPassword(ResetPasswordRequest requestData)
        {
            if (JwtTokenHelper.IsTokenExpired(requestData.Token, _jwtSettings?.Key!))
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.LinkExpired);
            }
            else
            {
                var userId = JwtTokenHelper.GetPrincipalFromExpiredToken(requestData.Token, _jwtSettings?.Key!).Claims.FirstOrDefault(x => x.Type == JWTClaimTypes.UserId)?.Value;
                if(userId == null)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Link is broken or corrupted !!");
                }
                else
                {
                    var res = await _userRepository.ResetPassword(userId, requestData.Password);
                    if (res)
                    {
                        return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.PasswordChanged);
                    }
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Password is not updated !");
                }
            }
        }

        public JsonResult RefreshRequest(RefreshRequest request)
        {
            if (JwtTokenHelper.IsTokenExpired(request.RefreshToken, _jwtSettings?.RefreshTokenKey!))
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TokenExpired);
            }

            var principal = JwtTokenHelper.GetPrincipalFromExpiredToken(request.AccessToken, _jwtSettings?.Key!);
            if (principal == null || !principal.Claims.Any())
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InvalidToken);
            }

            var newAccessToken = JwtTokenHelper.GenerateToken(
                principal.Claims,
                DateTime.Now.AddMinutes(AuthTokenExpiryInMinutes),
                _jwtSettings?.Key!,
                _jwtSettings?.Issuer!,
                _jwtSettings?.Audience!

            );

            var data = new { Token = newAccessToken };

            return HttpStatusCodeResponse.SuccessResponse(data, ResponseMessages.TokenGeneratedSuccessfully);
        }

        #region token generate
        private List<string> GetAccessTokenAndRefreshToken(UserResponse user)
        {
            List<string> data = new List<string>();
            Claim[] claims =
                {
                    new Claim(JWTClaimTypes.UserId, user.Id.ToString()),
                    new Claim(JWTClaimTypes.UserName, user.UserName),
                    new Claim(JWTClaimTypes.Email, user.Email),
                    new Claim(JWTClaimTypes.Role, user.RoleId.ToString()),
                    new Claim(JWTClaimTypes.CompanyId, user.CompanyId.ToString()),
                };

            string accessToken = JwtTokenHelper.GenerateToken(
                 claims,
                 DateTime.Now.AddMinutes(AuthTokenExpiryInMinutes),
                 _jwtSettings?.Key!,
                 _jwtSettings?.Issuer!,
                 _jwtSettings?.Audience!
             );

            string refreshToken = JwtTokenHelper.GenerateToken(
                [],
                DateTime.Now.AddDays(RefreshTokenExpiryInDays),
                _jwtSettings?.RefreshTokenKey!,
                _jwtSettings?.Issuer!,
                _jwtSettings?.Audience!
            );
            data.AddRange([accessToken, refreshToken]);
            return data;
        }
        #endregion

        #region Send ForgotPassword Email
        private async Task<bool> SendResetPasswordEmail(Guid userId, string email, string userName, string companyName)
        {
            string token = JwtTokenHelper.GenerateToken(
                [new Claim(JWTClaimTypes.UserId, userId.ToString())],
                DateTime.Now.AddMinutes(AuthTokenExpiryInMinutes),
                _jwtSettings?.Key!,
                _jwtSettings?.Issuer!,
                _jwtSettings?.Audience!
            );

            // Append the token directly to the URL
            string finalUrl = $"{_payrollWebSetting?.WebUrl}/auth/reset-password/{token}";

            string templatePath = FileHandler.EmailTemplatePath("ResetPassword.html");
            string bodyTemplate = await FileHandler.ReadFileContent(templatePath);

            string emailBody = bodyTemplate
                            .Replace("{fullName}", userName ?? "User")
                            .Replace("{companyName}", companyName)
                            .Replace("{passwordResetLink}", $"<a href='{finalUrl}' target='_blank'>Click here</a>");

            MailMessage mailMessage = new MailMessage
            {
                Body = emailBody,
                Subject = "Premium Pay Password Reset",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);
            return await _emailService.SendMail(mailMessage);

        }
        #endregion
    }
}
