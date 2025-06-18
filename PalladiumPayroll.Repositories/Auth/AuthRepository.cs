using Dapper;
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
using System.Net;
using System.Security.Claims;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _dapper;
        private readonly IConfiguration _configuration;
        public AuthRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
            _configuration = configuration;
        }

        public async Task<JsonResult> Login(LoginRequest loginRequest)
        {
            CommonResponse<TokenDTO>? response = new CommonResponse<TokenDTO>();

            try
            {
                // Fetch user by email
                DynamicParameters? parameters = new DynamicParameters();
                parameters.Add("@Email", loginRequest.Email);

                dynamic? user = await _dapper.ExecuteStoredProcedureSingle<dynamic>("sp_getUserDetailsByEmail", parameters);

                if (user == null)
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.NotFound,
                        message: ResponseMessages.User,
                        data: string.Empty
                    );
                }

                // Verify password
                PasswordHasher<object>? hasher = new PasswordHasher<object>();
                dynamic? verificationResult = hasher.VerifyHashedPassword(null, user.passwordHash, loginRequest.Password);

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

                Claim[] claims =
                {
                    new Claim(JWTClaimTypes.UserId, user.UserId),
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

                response.data = new TokenDTO
                {
                    Token = accessToken,
                    RefreshToken = refreshToken
                };

                return HttpStatusCodeResponse.GenerateResponse(
                   result: true,
                   HttpStatusCode.OK,
                   ResponseMessages.LoginSuccessfully,
                   response
               );
            }
            catch (Exception)
            {
                // Log the exception
                return HttpStatusCodeResponse.InternalServerErrorResponse(message : "An error occurred on the server");
            }
        }
    }
}
