using Dapper;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;
using System.Data;

namespace PalladiumPayroll.Repositories.CompanySettings
{
    public class PasswordPolicyRepository : IPasswordPolicyRepository
    {
        private readonly DapperContext _dapper;

        public PasswordPolicyRepository(DapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<List<PasswordPolicyResponseDTO>> GetPasswordPolicyByCompanyId(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var paramsfe = new DynamicParameters();

            var result = await _dapper.ExecuteStoredProcedure<PasswordPolicyResponseDTO>(
                "usp_GetPasswordPolicyByCompanyId", parameters);
            return result.ToList();
        }

        public async Task<long> CreatePasswordPolicy(PasswordPolicyRequestDTO request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PolicyName", request.PolicyName, dbType: DbType.String);
            parameters.Add("@MinLength", request.MinLength, dbType: DbType.Int32);
            parameters.Add("@MaxLength", request.MaxLength, dbType: DbType.Int32);
            parameters.Add("@NoOfUppercaseLetters", request.NoOfUppercaseLetters, dbType: DbType.Int32);
            parameters.Add("@NoOfDigits", request.NoOfDigits, dbType: DbType.Int32);
            parameters.Add("@NoOfSpecialLetters", request.NoOfSpecialLetters, dbType: DbType.Int32);
            parameters.Add("@PasswordAgeInterval", request.PasswordAgeInterval, dbType: DbType.Int32);
            parameters.Add("@SessionTimeOutInterval", request.SessionTimeOutInterval, dbType: DbType.Int32);
            parameters.Add("@IsInactive", request.IsInactive, dbType: DbType.Boolean);
            parameters.Add("@IsApplied", request.IsApplied, dbType: DbType.Boolean);
            parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64);
            parameters.Add("@ModifiedBy", request.ModifiedBy, dbType: DbType.String);
            parameters.Add("@PasswordPolicyId", dbType: DbType.Int64, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedure<object>(
                "usp_CreatePasswordPolicy", parameters);

            return parameters.Get<long>("@PasswordPolicyId");
        }

        public async Task<long> UpdatePasswordPolicy(PasswordPolicyRequestDTO request, long passwordPolicyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PasswordPolicyId", passwordPolicyId, dbType: DbType.Int64);
            parameters.Add("@PolicyName", request.PolicyName, dbType: DbType.String);
            parameters.Add("@MinLength", request.MinLength, dbType: DbType.Int32);
            parameters.Add("@MaxLength", request.MaxLength, dbType: DbType.Int32);
            parameters.Add("@NoOfUppercaseLetters", request.NoOfUppercaseLetters, dbType: DbType.Int32);
            parameters.Add("@NoOfDigits", request.NoOfDigits, dbType: DbType.Int32);
            parameters.Add("@NoOfSpecialLetters", request.NoOfSpecialLetters, dbType: DbType.Int32);
            parameters.Add("@PasswordAgeInterval", request.PasswordAgeInterval, dbType: DbType.Int32);
            parameters.Add("@SessionTimeOutInterval", request.SessionTimeOutInterval, dbType: DbType.Int32);
            parameters.Add("@IsInactive", request.IsInactive, dbType: DbType.Boolean);
            parameters.Add("@IsApplied", request.IsApplied, dbType: DbType.Boolean);
            parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64);
            parameters.Add("@ModifiedBy", request.ModifiedBy, dbType: DbType.String);
            parameters.Add("@Result", dbType: DbType.Int64, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedure<object>(
                "usp_UpdatePasswordPolicy", parameters);

            return parameters.Get<long>("@Result");
        }

        public async Task<long> DeletePasswordPolicy(long passwordPolicyId, long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PasswordPolicyId", passwordPolicyId, dbType: DbType.Int64);
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);
            parameters.Add("@Result", dbType: DbType.Int64, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedure<object>(
                "usp_DeletePasswordPolicy", parameters);

            return parameters.Get<long>("@Result");
        }
    }
}