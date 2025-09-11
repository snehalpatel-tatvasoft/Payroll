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
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            List<DepartmentResponseDTO>? departments = await _dapper.ExecuteStoredProcedure<DepartmentResponseDTO>(
                "usp_GetDepartmentsByCompanyId",
                parameters
            );

            return departments;
        }

        public async Task<long> CreateDepartment(DepartmentRequestDTO request)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", request.CompanyId);
            parameters.Add("@DepartmentName", request.DepartmentName);

            long departmentId = await _dapper.ExecuteStoredProcedureSingle<long>("usp_CreateDepartment", parameters);
            return departmentId;
        }

        public async Task<bool> EditDepartment(long departmentId, DepartmentRequestDTO request)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", departmentId);
            parameters.Add("@DepartmentName", request.DepartmentName);

            int rowsAffected = await _dapper.ExecuteAsync(
                "usp_UpdateDepartment",
                parameters
            );

            return rowsAffected > 0;
        }

        public async Task<(bool isSuccess, string message)> DeleteDepartment(long departmentId, long? employeeId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", departmentId);
            parameters.Add("@EmployeeId", employeeId);
            parameters.Add("@ResultMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            await _dapper.ExecuteAsync(
                "usp_DeleteDepartment",
                parameters
            );

            string message = parameters.Get<string>("@ResultMessage");

            bool success = message.Contains("successfully", StringComparison.OrdinalIgnoreCase);

            return (success, message);
        }

        public async Task<bool> CheckDepartmentNameExists(long companyId, string departmentName)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            parameters.Add("@DepartmentName", departmentName);
            parameters.Add("@Exists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.ExecuteAsync(
                "usp_CheckDepartmentNameExists",
                parameters
            );

            return parameters.Get<bool>("@Exists");
        }
    }
}