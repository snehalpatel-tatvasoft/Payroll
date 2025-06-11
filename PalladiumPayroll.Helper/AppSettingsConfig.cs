using Microsoft.Extensions.Configuration;

namespace PalladiumPayroll.Helper
{
    public class AppSettingsConfig
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString(name: "ConnectionString");
        }
    }
}
