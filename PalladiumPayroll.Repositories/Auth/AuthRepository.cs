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
using System.Security.Claims;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings? _jwtSettings;

        public AuthRepository(IConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtSettings = AppSettingsConfig.GetSection<JwtSettings>(configuration, "Jwt");
        }

        public async Task<JsonResult> Login(LoginRequest loginRequest)
        {
            try
            {
                // Fetch user by email
                UserResponse? user = await _userRepository.GetUserInfo(loginRequest.Email);

                if (user == null)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(
                        message: ResponseMessages.UserNotFound
                    );
                }
                else if (!user.ConfirmedEmail)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(
                        message: ResponseMessages.AccountNotConfirmed
                    );
                }

                // Verify password
                PasswordHasher<object>? hasher = new PasswordHasher<object>();
                var verificationResult = hasher.VerifyHashedPassword(null, user.PasswordHash, loginRequest.Password);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(
                        message: "Invalid credentials!"
                    );
                }

                // Generate JWT & Refresh token

                List<CompanyDetails>? companies = await _userRepository.GetCompaniesByEmail(loginRequest.Email);

                Claim[] claims =
                {
                    new Claim(JWTClaimTypes.UserId, user.Id.ToString()),
                    new Claim(JWTClaimTypes.UserName, user.UserName),
                    new Claim(JWTClaimTypes.Email, user.Email),
                    new Claim(JWTClaimTypes.Role, user.RoleId),
                    new Claim(JWTClaimTypes.CompanyId, user.CompanyId),
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

                var data = new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    Companies = companies
                };

                await _userRepository.LoginUser(user.Id.ToString());

                return HttpStatusCodeResponse.SuccessResponse(
                   data,
                   ResponseMessages.LoginSuccessfully
               );
            }
            catch (Exception)
            {
                // Log the exception
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "Internal server error!!");
            }
        }

        public JsonResult RefreshRequest(RefreshRequest request)
        {
            if (JwtTokenHelper.IsTokenExpired(request.RefreshToken, _jwtSettings?.RefreshTokenKey!))
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InvalidToken);
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
    }
}
