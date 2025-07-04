using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using PalladiumPayroll.Helper;

namespace PalladiumPayroll.DataContext
{
    public class DapperContext
    {
        private readonly string? _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = AppSettingsConfig.GetConnectionString(configuration);
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<List<T>> ExecuteStoredProcedure<T>(string storedProcedureName, DynamicParameters? parameters = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return (await db.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<T?> ExecuteStoredProcedureSingle<T>(string storedProcedureName, DynamicParameters? parameters = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> ExecuteStoredProcedureMultipleAsync<T>(string storedProcedureName, DynamicParameters? parameters, Func<SqlMapper.GridReader, Task<T>> mapFunc)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                using (var multi = await db.QueryMultipleAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure))
                {
                    return await mapFunc(multi);
                }
            }
        }

        public static async Task<bool> CheckDBConnection(string connectionString)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    await db.OpenAsync();
                    return db.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
