using Dapper;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Admin;
using System.Data;

namespace PalladiumPayroll.Repositories.Admin
{
    public class UserCreationRepository : IUserCreationRepository
    {
        private readonly DapperContext _dapper;

        public UserCreationRepository(DapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<int> CreateUser(UserCreationRequestDTO request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", request.UserName, dbType: DbType.String);
            parameters.Add("@SurName", request.SurName, dbType: DbType.String);
            parameters.Add("@AccessRoleID", request.AccessRoleID, dbType: DbType.Int32);
            parameters.Add("@ContactNo", request.ContactNo, dbType: DbType.String);
            parameters.Add("@Email", request.Email, dbType: DbType.String);
            parameters.Add("@Password", request.Password, dbType: DbType.String);
            parameters.Add("@PasswordHash", request.PasswordHash, dbType: DbType.String);
            parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64);
            parameters.Add("@IsDuplicateEmail", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            var result = await _dapper.ExecuteStoredProcedureSingle<int>(
                "usp_CreateUserForUserCreation", parameters);

            var isDuplicateEmail = parameters.Get<bool>("@IsDuplicateEmail");
            if (isDuplicateEmail)
            {
                return -1; // Indicate duplicate email
            }

            return result;
        }

        public async Task<int> UpdateUser(UserCreationRequestDTO request, Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Guid);
            parameters.Add("@UserName", request.UserName, dbType: DbType.String);
            parameters.Add("@SurName", request.SurName, dbType: DbType.String);
            parameters.Add("@AccessRoleID", request.AccessRoleID, dbType: DbType.Int32);
            parameters.Add("@ContactNo", request.ContactNo, dbType: DbType.String);
            parameters.Add("@Email", request.Email, dbType: DbType.String);
            parameters.Add("@Password", request.Password, dbType: DbType.String);
            parameters.Add("@PasswordHash", request.PasswordHash, dbType: DbType.String);
            parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64);
            parameters.Add("@IsDuplicateEmail", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            var result = await _dapper.ExecuteStoredProcedureSingle<int>(
                "usp_UpdateUserForUserCreation", parameters);

            var isDuplicateEmail = parameters.Get<bool>("@IsDuplicateEmail");
            if (isDuplicateEmail)
            {
                return -1; // Indicate duplicate email
            }

            return result;
        }

        public async Task<int> DeleteUser(Guid id, long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Guid);
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedureSingle<int>(
                "usp_DeleteUserForUserCreation", parameters);

            return result;
        }

        public async Task<List<UserListResponseDTO>> GetUsersByCompanyId(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<UserListResponseDTO>(
                "usp_GetUsersByCompanyId", parameters);
            return result.ToList();
        }
    }
}