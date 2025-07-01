using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Services.Applicationadmin;
using PalladiumPayroll.Services.Company;
using PalladiumPayroll.Services.Home;
using PalladiumPayroll.Services.User;

namespace PalladiumPayroll.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IApplicationadminService, ApplicationadminService>();
            services.AddScoped<EmailService>();
            return services;
        }
    }
}
