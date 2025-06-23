using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Repositories.Company
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _dapper;
        public CompanyRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

        public async Task<long> CreateCompany(CreateCompanyRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CompanyName", request.Company);
            parameters.Add("@NoOfEmployee", request.NoOfEmployee);
            parameters.Add("@Country", request.Country);

            long companyId = await _dapper.ExecuteStoredProcedureSingle<long>("sp_createCompany", parameters);
            return companyId;
        }

        public async Task<bool> CreateUser(CreateUserRequestDto request)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@UserName", request.FirstName);
            parameters.Add("@SurName", request.LastName);
            parameters.Add("@Email", request.Email);
            parameters.Add("@Password", request.Password);
            parameters.Add("@PasswordHash", request.PasswordHash);
            parameters.Add("@ContactNo", request.ContactNo);
            parameters.Add("@CompanyId", request.CompanyId);

            bool result = await _dapper.ExecuteStoredProcedureSingle<bool>("sp_createUser", parameters);
            return result;
        }

        public async Task<bool> CheckCompanyExist(string company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyName", company);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("sp_CheckCompanyExists", parameters);
            return response;
        }
    }
}
