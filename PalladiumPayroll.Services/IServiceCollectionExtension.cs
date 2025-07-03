using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Services.Auth;
using PalladiumPayroll.Services.Company;
using PalladiumPayroll.Services.Department;
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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            return services;
        }
    }
}
