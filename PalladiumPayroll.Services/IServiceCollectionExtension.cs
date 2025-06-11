using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Services.Home;

namespace PalladiumPayroll.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IHomeService, HomeService>();
            return services;
        }
    }
}
