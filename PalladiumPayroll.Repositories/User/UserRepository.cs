using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Admin;
using PalladiumPayroll.DTOs.Miscellaneous.Constants;
using PalladiumPayroll.Helper;

namespace PalladiumPayroll.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dapper = new DapperContext(configuration);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CheckEmailExist(string email)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CheckEmailExists", parameters);
            return response;
        }

        public async Task<List<UserResponse>> GetUserInfo(string email)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            return await _dapper.ExecuteStoredProcedure<UserResponse>("usp_GetUserDetailsByEmail1", parameters);
        }

        public async Task<bool> ResetPassword(string userId, string password)
        {
            string passwordHash = new PasswordHasher<object>().HashPassword(user: string.Empty, password);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@Password", SecurityHandler.Encrypt(password));
            parameters.Add("@HasPassword", passwordHash);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_ResetUserPassword", parameters);
        }

        public async Task<UserResponse?> GetUserInfoByUserId(string userId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            return await _dapper.ExecuteStoredProcedureSingle<UserResponse>("usp_GetUserDetailsByUserId", parameters);
        }


        public async Task<bool> ConfirmEmail(string userId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_ConfirmUserEmail", parameters);
            return response;
        }

        public async Task<bool> CheckIsUserLoggedIn(string userId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CheckIsUserLoggedIn", parameters);
            return response;
        }

        public async Task<bool> UpdateLastActivity(string userId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            var response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdateUserLastActivity", parameters);
            return response;
        }

        public async Task<bool> UpdateUserIsLogin(string userId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            var response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdateIsLoggedInFlag", parameters);
            return response;
        }

        public async Task<bool> LogoutInactiveUsers()
        {
            var response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdateInActiveUsers");
            return response;
        }

        public async Task<List<CompanyDetails>> GetCompaniesByEmail(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            List<CompanyDetails> result = await _dapper.ExecuteStoredProcedure<CompanyDetails>("usp_GetCompaniesByUserEmail", parameters);
            return result;
        }

        public async Task<int> UpdateUserEmail(string email, string newEmail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            parameters.Add("@NewEmail", newEmail);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<int>("usp_UpdateUserEamil", parameters);
        }

        public async Task<string?> GetUserPassword()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<string>("usp_GetUserPassword", parameters);
        }

        public async Task<bool> UpdateUserPassword(string newPassword)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value;
            return await ResetPassword(userId!, newPassword);
        }

        public async Task<UserHeaderModel?> UserHeaderInfo()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<UserHeaderModel>("usp_GetUserHeaderInfo", parameters);
        }

        public async Task<bool> ReceiveEmailNotification(bool isReceive)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            parameters.Add("@IsReceive", isReceive);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertUserIsReceiveEmail", parameters);
        }
    }
}
