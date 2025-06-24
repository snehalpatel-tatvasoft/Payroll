using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PalladiumPayroll.Helper.License;
using PalladiumPayroll.Helper.Log4net;

namespace PalladiumPayroll.Helper
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services, IConfiguration configuration)
        {
            // Register strongly-typed config
            services.Configure<PayrollMachineData>(configuration.GetSection("PayrollMachine"));

            // Register class that uses it
            services.AddScoped<PayrollMachine>();

            services.AddTransient<ILog4net, Log4net.Log4net>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
