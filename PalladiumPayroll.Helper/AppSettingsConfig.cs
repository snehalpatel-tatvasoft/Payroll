using Microsoft.Extensions.Configuration;

namespace PalladiumPayroll.Helper
{
    public class AppSettingsConfig
    {
        public static string? GetConnectionString(IConfiguration configuration)
        {
            string _connectionString = configuration.GetConnectionString(name: "ConnectionString");
            return _connectionString;
        }
    }
}
