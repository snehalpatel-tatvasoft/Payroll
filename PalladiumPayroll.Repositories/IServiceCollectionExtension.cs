using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Repositories.Applicationadmin;
using PalladiumPayroll.Repositories.Auth;
using PalladiumPayroll.Repositories.Comany_Settings;
using PalladiumPayroll.Repositories.Company;
using PalladiumPayroll.Repositories.CompanySettings;
using PalladiumPayroll.Repositories.CompanySettings.CreateTransaction;
using PalladiumPayroll.Repositories.Department;
using PalladiumPayroll.Repositories.Employees;
using PalladiumPayroll.Repositories.EmployeesLoan;
using PalladiumPayroll.Repositories.Employees;
using PalladiumPayroll.Repositories.Home;
using PalladiumPayroll.Repositories.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeGrievances;
using PalladiumPayroll.Repositories.User;
using PalladiumPayroll.Repositories.HRFunctions.EmployeePromotions;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeTraining;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeTransfer;

namespace PalladiumPayroll.Repositories
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicationadminRepository, ApplicationadminRepository>();
            services.AddScoped<IDesignationsRepository, DesignationsRepository>();
            services.AddScoped<IMinimumWageRepository, MinimumWageRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeCodesRepository, EmployeeCodesRepository>();
            services.AddScoped<ICustomizeReportRepository, CustomizeReportRepository>();
            services.AddScoped<IPayslipDisplaySetupRepository, PayslipDisplaySetupRepository>();
            services.AddScoped<ITimesheetSetupRepository, TimesheetSetupRepository>();
            services.AddScoped<ICreateTransactionRepository, CreateTransactionRepository>();
            services.AddScoped<IPasswordPolicyRepository, PasswordPolicyRepository>();
            services.AddScoped<IEmployeeGrievancesRepository, EmployeeGrievancesRepository>();
            services.AddScoped<INotificationSetupRepository, NotificationSetupRepository>();
            services.AddScoped<IEmployeesLoanRepository, EmployeesLoanRepository>();
            services.AddScoped<IEmployeePromotionsRepository, EmployeePromotionsRepository>();
            services.AddScoped<IEmployeeTrainingRepository, EmployeeTrainingRepository>();
            services.AddScoped<IEmployeeTransferRepository, EmployeeTransferRepository>();
            services.AddScoped<IDisciplinaryLogRepository, DisciplinaryLogRepository>();
            return services;
        }
    }
}
