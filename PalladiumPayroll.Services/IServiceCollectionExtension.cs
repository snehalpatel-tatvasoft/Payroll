using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Repositories.Admin.AccessRights;
using PalladiumPayroll.Services.Admin.AccessRights;
using PalladiumPayroll.Repositories.Admin;
using PalladiumPayroll.Services.Admin;
using PalladiumPayroll.Services.Applicationadmin;
using PalladiumPayroll.Services.Auth;
using PalladiumPayroll.Services.Company;
using PalladiumPayroll.Services.Company_Settings;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.Services.CompanySettings.CreateTransaction;
using PalladiumPayroll.Services.Department;
using PalladiumPayroll.Services.EmployeesLoan;
using PalladiumPayroll.Services.Employees;
using PalladiumPayroll.Services.Home;
using PalladiumPayroll.Services.HRFunctions.EmployeeGrievances;
using PalladiumPayroll.Services.HRFunctions.EmployeePromotions;
using PalladiumPayroll.Services.HRFunctions.EmployeeTraining;
using PalladiumPayroll.Services.HRFunctions.EmployeeTransfer;
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
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationadminService, ApplicationadminService>();
            services.AddScoped<EmailService>();
            services.AddScoped<IDesignationsService, DesignationsService>();
            services.AddScoped<IMinimumWageService, MinimumWageService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeCodesService, EmployeeCodesService>();
            services.AddScoped<ICustomizeReportService, CustomizeReportService>();
            services.AddScoped<IPayslipDisplaySetupService, PayslipDisplaySetupService>();
            services.AddScoped<ITimesheetSetupService, TimesheetSetupService>();
            services.AddScoped<ICreateTransactionService, CreateTransactionService>();
            services.AddScoped<IPasswordPolicyService, PasswordPolicyService>();
            services.AddScoped<IEmployeeGrievancesService, EmployeeGrievancesService>();
            services.AddScoped<INotificationSetupService, NotificationSetupService>();
            services.AddScoped<IEmployeesLoanService, EmployeesLoanService>();
            services.AddScoped<IEmployeePromotionsService, EmployeePromotionsService>();
            services.AddScoped<IEmployeeTrainingService, EmployeeTrainingService>();
            services.AddScoped<IEmployeeTransferService, EmployeeTransferService>();
            services.AddScoped<IAccessRightsService, AccessRightsService>();
            services.AddScoped<IUserCreationService, UserCreationService>();
            return services;
        }
    }
}
