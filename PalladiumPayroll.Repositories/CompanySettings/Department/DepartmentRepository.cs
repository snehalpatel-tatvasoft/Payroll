using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Repositories.Department
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DapperContext _dapper;

        public DepartmentRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

        public async Task<List<DepartmentResponseDTO>> GetDepartmentsByCompanyId(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            var departments = await _dapper.ExecuteStoredProcedure<DepartmentResponseDTO>(
                "usp_GetDepartmentsByCompanyId",
                parameters
            );

            return departments;
        }

        public async Task<long> CreateDepartment(DepartmentRequestDTO request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", request.CompanyId);
            parameters.Add("@DepartmentName", request.DepartmentName);

            long departmentId = await _dapper.ExecuteStoredProcedureSingle<long>("usp_CreateDepartment", parameters);
            return departmentId;
        }

        public async Task<bool> EditDepartment(long departmentId, DepartmentRequestDTO request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", departmentId);
            parameters.Add("@DepartmentName", request.DepartmentName);

            int rowsAffected = await _dapper.CreateConnection().ExecuteAsync(
                "usp_UpdateDepartment",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteDepartment(long departmentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", departmentId);

            int rowsAffected = await _dapper.CreateConnection().ExecuteAsync(
                "usp_DeleteDepartment",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }

        public async Task<bool> CheckDepartmentNameExists(long companyId, string departmentName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            parameters.Add("@DepartmentName", departmentName);
            parameters.Add("@Exists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.CreateConnection().ExecuteAsync(
                "usp_CheckDepartmentNameExists",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<bool>("@Exists");
        }
    }
}