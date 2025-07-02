using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.DTOs.Miscellaneous.Constants;
using PalladiumPayroll.Helper.Constants;
using PalladiumPayroll.Helper.JWTToken;
using PalladiumPayroll.Repositories.User;
using System.ComponentModel.Design;
using System.Net;
using System.Security.Claims;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _dapper;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthRepository(IConfiguration configuration, IUserRepository userRepository)
        {
            _dapper = new DapperContext(configuration);
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<JsonResult> Login(LoginRequest loginRequest)
        {
            try
            {
                // Fetch user by email
                UserResponse? user = await _userRepository.GetUserInfo(loginRequest.Email);

                if (user == null)
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.NotFound,
                        message: ResponseMessages.UserNotFound,
                        data: string.Empty
                    );
                }
                else if (!user.ConfirmedEmail)
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.NotFound,
                        message: ResponseMessages.AccountNotConfirmed,
                        data: string.Empty
                    );
                }

                // Verify password
                PasswordHasher<object>? hasher = new PasswordHasher<object>();
                dynamic? verificationResult = hasher.VerifyHashedPassword(null, user.PasswordHash, loginRequest.Password);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.Unauthorized,
                        message: "Invalid credentials!",
                        data: string.Empty
                    );
                }

                // Generate JWT & Refresh token
                JWTTokenService jwtService = new JWTTokenService(_configuration);

                List<CompanyDetails>? companies = await _userRepository.GetCompaniesByEmail(loginRequest.Email);

                Claim[] claims =
                {
                    new Claim(JWTClaimTypes.UserId, user.Id.ToString()),
                    new Claim(JWTClaimTypes.UserName, user.UserName),
                    new Claim(JWTClaimTypes.Email, user.Email),
                    new Claim(JWTClaimTypes.Role, user.RoleId),
                };

                string accessToken = jwtService.GenerateToken(
                     claims,
                     DateTime.Now.AddMinutes(AppConstants.AuthTokenExpiryInMinutes),
                     _configuration["Jwt:Key"]!
                 );

                string refreshToken = jwtService.GenerateToken(
                    claims,
                    DateTime.Now.AddDays(AppConstants.RefreshTokenExpiryInDays),
                    _configuration["Jwt:RefreshTokenKey"]!
                );

                var data = new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    Companies = companies
                };

                await _userRepository.LoginUser(user.Id.ToString());

                return HttpStatusCodeResponse.GenerateResponse(
                   result: true,
                   HttpStatusCode.OK,
                   ResponseMessages.LoginSuccessfully,
                   data
               );
            }
            catch (Exception)
            {
                // Log the exception
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "Internal server error!!");
            }
        }
    }
}
