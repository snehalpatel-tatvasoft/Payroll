using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Services.Company;
using PalladiumPayroll.Services.Home;

namespace PalladiumPayroll.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<ICompanyService, CompanyService>();
            return services;
        }
    }
}
