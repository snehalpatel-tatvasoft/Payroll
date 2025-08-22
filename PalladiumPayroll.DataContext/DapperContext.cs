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
        private static readonly int SQLCommandTimeOut = 30;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = AppSettingsConfig.GetConnectionString(configuration);
            _connectionString = string.IsNullOrEmpty(_connectionString) ? _connectionString : SecurityHandler.Decrypt(_connectionString, "U4%");
        }

        private IDbConnection CreateConnection(string? connectionString = null) => new SqlConnection(string.IsNullOrEmpty(connectionString) ? _connectionString :  connectionString);

        public async Task<List<T>> ExecuteStoredProcedure<T>(string storedProcedureName, DynamicParameters? parameters = null)
        {
            using (IDbConnection db = CreateConnection())
            {
                return (await db.QueryAsync<T>(storedProcedureName, parameters, commandTimeout: SQLCommandTimeOut, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<T?> ExecuteStoredProcedureSingle<T>(string storedProcedureName, DynamicParameters? parameters = null)
        {
            using (IDbConnection db = CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<T>(storedProcedureName, parameters, commandTimeout: SQLCommandTimeOut, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> ExecuteStoredProcedureMultipleAsync<T>(string storedProcedureName, DynamicParameters? parameters, Func<SqlMapper.GridReader, Task<T>> mapFunc)
        {
            using (IDbConnection db = CreateConnection())
            {
                using (var multi = await db.QueryMultipleAsync(storedProcedureName, parameters, commandTimeout: SQLCommandTimeOut, commandType: CommandType.StoredProcedure))
                {
                    return await mapFunc(multi);
                }
            }
        }

        public async Task<int> ExecuteAsync(string storedProcedureName, DynamicParameters? parameters = null)
        {
            using (IDbConnection db = CreateConnection())
            {
                return await db.ExecuteAsync(storedProcedureName, parameters, commandTimeout: SQLCommandTimeOut, commandType: CommandType.StoredProcedure);
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

        public async Task<List<T>> ExecuteQueryWithConnection<T>(string query, string connectionString)
        {
            using (IDbConnection db = CreateConnection(connectionString))
            {
                return (await db.QueryAsync<T>(query, commandTimeout: SQLCommandTimeOut)).ToList();
            }
        }

    }
}
