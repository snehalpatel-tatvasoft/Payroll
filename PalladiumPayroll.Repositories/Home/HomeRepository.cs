using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs;
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

        public async Task<List<Employee>> GetAllEmployeeList(int employeeId)
        {
            using (var conn = _context.CreateConnection())
            {
                DynamicParameters parameters = new();
                parameters.Add("@CompanyId", employeeId);

                var data = await conn.QueryAsync<Employee>("SP_FetchEmployeeList", parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
    }
}
