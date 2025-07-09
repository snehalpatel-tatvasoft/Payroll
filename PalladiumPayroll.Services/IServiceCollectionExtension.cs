using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Services.Applicationadmin;
using PalladiumPayroll.Services.Auth;
using PalladiumPayroll.Services.Company;
using PalladiumPayroll.Services.Company_Settings;
using PalladiumPayroll.Services.CompanySettings;
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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationadminService, ApplicationadminService>();
            services.AddScoped<EmailService>();
            services.AddScoped<IDesignationsService, DesignationsService>();
            services.AddScoped<IMinimumWageService,MinimumWageService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICustomizeReportService, CustomizeReportService>();
            return services;
        }
    }
}
