using PalladiumPayroll.DataContext;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DTOs.Miscellaneous;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;
using PalladiumPayroll.Helper;

namespace PalladiumPayroll.Repositories.Home
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DapperContext _context;
        private readonly string _connectionString;

        public HomeRepository(IConfiguration configuration)
        {
            _connectionString = AppSettingsConfig.GetConnectionString(configuration);
        }

        public List<T> ExecuteStoredProcedure<T>(string SPName, DynamicParameters DP)
        {
            using (IDbConnection? connection = _context.CreateConnection())
            {
                return connection.Query<T>(SPName, DP).ToList();
            }
        }

        public async Task<ApiResponse<object>> GetAllEmployeeList(int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@params", employeeId);

                    await conn.ExecuteAsync("sp_xyz", parameters, commandType: CommandType.StoredProcedure);

                    string statusMsg = string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieving);

                    return new ApiResponse<object>(isSuccess: true, statusMsg, data: null);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>(isSuccess: false, string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message), data: null);
            }
        }
    }
}
