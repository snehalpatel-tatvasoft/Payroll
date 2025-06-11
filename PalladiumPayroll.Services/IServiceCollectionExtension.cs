using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Services.Home;

namespace PalladiumPayroll.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            return services;
        }
    }
}
