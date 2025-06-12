using Microsoft.Extensions.Configuration;

namespace PalladiumPayroll.Helper
{
    public static class AppSettingsConfig
    {
        public static string? GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString(name: "DefaultConnection");
        }

        public static T? GetSection<T>(IConfiguration configuration, string sectionName)
        {
            return configuration.GetSection(sectionName).Get<T>();
        }

        public static string? GetValue(IConfiguration configuration, string keyName)
        {
            return configuration.GetValue<string>(keyName);
        }

    }
}
