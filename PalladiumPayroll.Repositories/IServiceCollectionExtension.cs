using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Repositories.Company;
using PalladiumPayroll.Repositories.Home;

namespace PalladiumPayroll.Repositories
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            return services;
        }
    }
}
