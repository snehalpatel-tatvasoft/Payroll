using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapper;
        public UserRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

        public async Task<bool> CheckEmailExist(string email)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("sp_CheckEmailExists", parameters);
            return response;
        }

        public async Task<UserResponse?> GetUserInfo(string email)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            return await _dapper.ExecuteStoredProcedureSingle<UserResponse>("sp_getUserDetailsByEmail", parameters);
        }

        public async Task<bool> ConfirmEmail(string userId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_confirmUserEmail", parameters);
            return response;
        }

        public async Task<bool> CheckIsUserLoggedIn(string userId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_checkIsUserLoggedIn", parameters);
            return response;
        }

        public async Task<bool> UpdateLastActivity(string userId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            var response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_updateUserLastActivity", parameters);
            return response;
        }
    }
}
