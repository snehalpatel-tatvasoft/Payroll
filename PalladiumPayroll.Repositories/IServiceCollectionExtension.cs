using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Repositories.Admin.AccessRights;
using PalladiumPayroll.Repositories.Admin;
using PalladiumPayroll.Repositories.Applicationadmin;
using PalladiumPayroll.Repositories.Auth;
using PalladiumPayroll.Repositories.Comany_Settings;
using PalladiumPayroll.Repositories.Company;
using PalladiumPayroll.Repositories.CompanySettings;
using PalladiumPayroll.Repositories.CompanySettings.CreateTransaction;
using PalladiumPayroll.Repositories.Department;
using PalladiumPayroll.Repositories.Employees;
using PalladiumPayroll.Repositories.EmployeesLoan;
using PalladiumPayroll.Repositories.Home;
using PalladiumPayroll.Repositories.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeGrievances;
using PalladiumPayroll.Repositories.User;
using PalladiumPayroll.Repositories.HRFunctions.EmployeePromotions;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeTraining;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeTransfer;
using PalladiumPayroll.Repositories.CheckInOut;
using PalladiumPayroll.Repositories.HRFunctions.CoidAccident;
using PalladiumPayroll.Repositories.Utilities.DataImport;
using PalladiumPayroll.Repositories.CompanySettings.LeaveSettings;
using PalladiumPayroll.Services.PayrollProcess.ManageLeave;
using PalladiumPayroll.Services.PayrollProcess.TimeSheet;
using PalladiumPayroll.Repositories.PayrollProcess.PieceWork;
using PalladiumPayroll.Repositories.CompanySettings.EmployeeProfile;

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
            services.AddScoped<IAccessRightsRepository, AccessRightsRepository>();
            services.AddScoped<IUserCreationRepository, UserCreationRepository>();
            services.AddScoped<IDisciplinaryLogRepository, DisciplinaryLogRepository>();
            services.AddScoped<ICheckInOutRepository, CheckInOutRepository>();
            services.AddScoped<ICoidAccidentRepository,CoidAccidentRepository>();
            services.AddScoped<IDataImportRepository, DataImportRepository>();
            services.AddScoped<ILeaveSettingsRepository, LeaveSettingsRepository>();
            services.AddScoped<IManageLeaveRepository, ManageLeaveRepository>();
            services.AddScoped<ITimeSheetRepository, TimeSheetRepository>();
            services.AddScoped<IPieceWorkRepository, PieceWorkRepository>();
            services.AddScoped<IEmployeeProfileRepository, EmployeeProfileRepository>();
            return services;
        }
    }
}
