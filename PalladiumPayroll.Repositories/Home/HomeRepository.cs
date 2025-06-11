using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Repositories.Home
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DapperContext _context;
        private readonly string _connectionString;

        public HomeRepository(IConfiguration configuration)
        {
            _context = new DapperContext(configuration);
            _connectionString = AppSettingsConfig.GetConnectionString(configuration);
        }

        public List<T> ExecuteStoredProcedure<T>(string SPName, DynamicParameters DP)
        {
            using (IDbConnection? connection = _context.CreateConnection())
            {
                return connection.Query<T>(SPName, DP).ToList();
            }
        }

        public async Task<JsonResult> GetAllEmployeeList(int employeeId)
        {
            try
            {
                using (var conn = _context.CreateConnection())
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@params", employeeId);

                    await conn.ExecuteAsync("sp_xyz", parameters, commandType: CommandType.StoredProcedure);

                    string statusMsg = string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieving);
                    var data = "";
                    return HttpStatusCodeResponse.SuccessResponse(data, statusMsg);
                }
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }
    }
}
