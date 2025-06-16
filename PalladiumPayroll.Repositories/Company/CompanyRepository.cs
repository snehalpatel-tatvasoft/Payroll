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

        public async Task<bool> CreateCompany(CreateCompanyRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@UserName", request.UserName);
            parameters.Add("@FirstName", request.FirstName);
            parameters.Add("@LastName", request.LastName);
            parameters.Add("@CompanyName", request.CompanyName);
            parameters.Add("@Country", request.Country);
            parameters.Add("@NoOfEmployee", request.NoOfEmployee);
            parameters.Add("@ContactNo", request.ContactNo);
            parameters.Add("@Email", request.Email);
            parameters.Add("@CreatedBy", request.CreatedBy);
            parameters.Add("@CreatedDate", request.CreatedDate);
            parameters.Add("@Password", request.Password);
            parameters.Add("@ConfirmPassword", request.ConfirmPassword);
            parameters.Add("@Term", request.Term);

            // Optional fields if they are part of the model:
            parameters.Add("@RoleId", request.RoleId);
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@EmployeeCode", request.EmployeeCode);
            parameters.Add("@Country", request.Countries);

            bool result = await _dapper.ExecuteStoredProcedureSingle<bool>("sp_name", parameters);
            return result;
        }

    }
}
