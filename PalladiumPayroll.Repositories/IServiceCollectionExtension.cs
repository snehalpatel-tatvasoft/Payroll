using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Repositories.Auth;
using PalladiumPayroll.Repositories.Company;
using PalladiumPayroll.Repositories.CompanySettings;
using PalladiumPayroll.Repositories.Home;
using PalladiumPayroll.Repositories.User;

namespace PalladiumPayroll.Repositories
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IMinimumWageRepository, MinimumWageRepository>();
            return services;
        }
    }
}
