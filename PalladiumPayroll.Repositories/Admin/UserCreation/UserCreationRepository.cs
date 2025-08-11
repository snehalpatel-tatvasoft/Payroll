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
            parameters.Add("@AccessRoleID", request.AccessRoleId, dbType: DbType.Int32);
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
            parameters.Add("@AccessRoleID", request.AccessRoleId, dbType: DbType.Int32);
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

        public async Task<List<AccessRoleResponseDTO>> GetAccessRolesNamesByCompanyId(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<AccessRoleResponseDTO>(
                "usp_GetAccessRolesNamesByCompanyIdForAdmin", parameters);
            return result.ToList();
        }

        public async Task<UserCreationRequestDTO> GetUserById(Guid id, long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Guid);
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedureSingle<UserCreationRequestDTO>(
                "usp_GetUserById", parameters);
            return result ?? new UserCreationRequestDTO();
        }


        public async Task<List<UserFunctionalityAccessRightsDTO>> GetUserFunctionalityById(Guid userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, DbType.Guid);
            // parameters.Add("@CompanyId", companyId, DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<UserFunctionalityAccessRightsDTO>(
                "usp_GetUserFunctionalityById", parameters);

            return result ?? new List<UserFunctionalityAccessRightsDTO>();
        }

        public async Task<bool> SaveUserFunctionalityAccessRights(List<SaveUserFunctionalityAccessRightsDTO> requests)
        {
            foreach (var request in requests)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", request.UserId, DbType.Guid);
                parameters.Add("@FunctionalityId", request.FunctionalityId, DbType.Int32);
                parameters.Add("@View", request.View, DbType.Boolean);
                parameters.Add("@Edit", request.Edit, DbType.Boolean);
                parameters.Add("@New", request.New, DbType.Boolean);
                parameters.Add("@Delete", request.Delete, DbType.Boolean);
                parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                await _dapper.ExecuteStoredProcedureSingle<object>(
                    "usp_SaveUserFunctionalityAccessRights", parameters);

                if (!parameters.Get<bool>("@IsSuccess"))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<List<UserPayFrequencyAccessRightsDTO>> GetUserPayFrequenciesById(Guid userId, long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, DbType.Guid);
            parameters.Add("@CompanyId", companyId, DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<UserPayFrequencyAccessRightsDTO>(
                "usp_GetUserPayFrequenciesById", parameters);

            return result ?? new List<UserPayFrequencyAccessRightsDTO>();
        }

        public async Task<bool> SaveUserPayFrequenciesAccessRights(List<SaveUserPayFrequencyAccessRightsDTO> requests)
        {
            foreach (var request in requests)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", request.UserId, DbType.Guid);
                parameters.Add("@CompanyPayrollId", request.CompanyPayrollId, DbType.Int64);
                parameters.Add("@IsAllow", request.IsAllow, DbType.Boolean);
                parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                await _dapper.ExecuteStoredProcedureSingle<object>(
                    "usp_SaveUserPayFrequenciesAccessRights", parameters);

                if (!parameters.Get<bool>("@IsSuccess"))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<List<UserFunctionalityAccessRightsDTO>> GetUserFunctionalityByIdForTransactionFunctions(Guid userId, long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, DbType.Guid);
            parameters.Add("@CompanyId", companyId, DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<UserFunctionalityAccessRightsDTO>(
                "usp_GetUserFunctionalityByIdForTransactionFunctions", parameters);

            return result ?? new List<UserFunctionalityAccessRightsDTO>();
        }
    }
}