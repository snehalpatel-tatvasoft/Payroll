
using System.Runtime.CompilerServices;

namespace PalladiumPayroll.Helper.Constants
{
    public static class AppConstants
    {
        public const int DefaultPageSize = 999999999;

        public const int AuthTokenExpiryInMinutes = 10;

        public const int RefreshTokenExpiryInDays = 7;

        public const int ResetPasswordTokenExpiryInDays = 1;

        public const int EmailVerificationTokenLength = 100;

        public const int ResetPasswordTokenLength = 100;

        public const int PageNumber = 1;
        
        public const string DefaultConnectionString = "Data Source={0}; initial catalog={1}; User ID={2}; Password={3}; TrustServerCertificate=True;";


        public static class ResponseMessages
        {
            #region Common

            public static readonly string Success = "{0} {1} Successfully.";
            public static readonly string NotFound = "{0} Not Found!";
            public static readonly string Exception = "An Error Occurred While {0} {1}: {2}";
            public static readonly string UnexpectedError = "An Unexpected error occurred";
            public static readonly string InvalidOrMissingRequestParameters = "Invalid or missing request parameters";
            public static readonly string UnAuthorized = "UnAuthorized Access !!";
            public static readonly string TryLater = "Plaese try again later.";
            public static readonly string AlreadyExist = "{0} already exists !";
            public static readonly string Valid = "{0} is valid";
            public static readonly string SomethingWrong = "Something went wrong.";

            #endregion

            #region Employee

            public static readonly string Employee = "Employee";

            #endregion

            #region Company

            public static readonly string Company = "Company";
            public const string CompanyAlreadyExists = "Company Already Exists";
            public const string CompanyRegisteredSuccessfully = "Company Registered Successfully";
            public const string ErrorCreatingCompany = "Error While Creating Company!!";
            public const string GLSetupError = "Error while connecting GL Database!!";
            public const string GLSetupSuccess = "GL database connected succssfully!!";

            #endregion

            #region User

            public static readonly string User = "User";
            public const string UserNotFound = "We couldn't find an account associated with this email address!";
            public const string EmailAlreadyExits = "Email Already Exists";
            public const string ErrorCreatingUser = "Error While Creating User!!";
            public const string LoggedOutDueToInActivity = "Your session has expired due to inactivity. Please log in again.";

            #endregion

            #region Authorization

            public const string LoginSuccessfully = "Login Successfully";
            public const string LoginPasswordMismatch = "Password Is Incorrect";
            public const string LinkExpired = "Link Is Expired!";
            public const string PasswordChanged = "Password Changed Successfully";
            public const string TokenExpired = "Token Expired";
            public const string InvalidToken = "Invalid refresh token";
            public const string ForcePasswordReset = "You need to reset your password";
            public const string UserInActive = "Sorry your account is InActive, you can't login!";
            public const string PlanExpired = "Your subscription has expired. Please renew to continue using the service.";
            public const string TokenGeneratedSuccessfully = "Token Generated Successfully";
            public const string InternalServerError = "Internal Server Error!!";


            #endregion

            #region Home
            public static readonly string PayrollSummary = "Payroll Summary data in dashboard";
            public static readonly string EmployeeTypeCount = "Employee type count in dashboard";
            public static readonly string PayrollCycle = "payroll cycles in dashboard";
            #endregion

            #region Email

            public const string EmailSentSuccessfully = "Confirmation email sent. Please click on the link to activate your account.";
            public const string EmailSentFailure = "Cannot Send Email";
            public const string EmailMailboxUnavailable = "Mailbox unavailable";
            public const string EmailVerified = "Email verified successfully. Please sign in to get started.";
            public const string AccountNotConfirmed = "Account not confirmed. Please contact the administrator to activate your account.";

            #endregion

            #region Application Admin
            public static readonly string AppAdminDashboard = "Application Admin Dashboard Data";


            #endregion

            #region Dashboard

            public static readonly string Dashboard = "Dashboard data";

            #endregion

            #region Designation

            public const string DesignationsCreatedSuccessfully = "Designation created successfully";
            public const string DesignationsCreationFailed = "Failed to create designation";
            public const string DataFetchSuccess = "Data fetched successfully";
            public const string DesignationsDeletedSuccessfully = "Designation deleted successfully";
            public const string DesignationsUpdatedSuccessfully = "Designation updated successfully";
            public const string DesignationsUpdateFailed = "Failed to update designation";
            public const string DesignationDuplicate = "Designation with the same Name and Code already exists.";

            #endregion

            #region Minimum Wage 

            public static readonly string MinimumWage = "Minimum Wage";
            public const string DuplicateMinimumWage = "Duplicate minimum wage name for this company.";
            public const string UnableToSaveMinimumWage = "Unable to save minimum wage.";
            public const string MinimumWageNotFound = "Minimum Wage entry could not be deleted or was not found.";
            public const string WageIdNotFound = "Wage Id is not found.";
            public const string CompanyIdNotFound = "Company Id is not found.";

            #endregion

            #region Department

            public static readonly string DepartmentsRetrievedSuccessfully = "Departments retrieved successfully.";
            public static readonly string NoDepartmentsForThisCompanyId = "No departments found for the specified company.";
            public static readonly string ErrorRetrievingDepartments = "Error retrieving departments.";
            public static readonly string CheckDuplicateDepartment = "This department name already exists. Please choose a different one.";
            public static readonly string DepartmentCreateSuccessfully = "Department created successfully.";
            public static readonly string DepartmentNameRequired = "Department name is required.";
            public static readonly string ErrorCreatingDepartment = "Error while creating the department.";
            public static readonly string DepartmentUpdateSuccessfully = "Department updated successfully.";
            public static readonly string ErrorUpdatingDepartment = "Error updating department.";
            public static readonly string ErrorDeletingDepartment = "Error deleting department.";
            public static readonly string DepartmentDeleteSuccessfully = "Department deleted successfully.";


            #endregion


            #region Employee Code

            public static readonly string EmployeeCode = "Employee Code";

            public const string EmployeeCodeSaveFailed  = "Failed to save employee code";

            #endregion


            #region Payslip Display Setup

            public static readonly string PayslipDisplaySetup = "Payslip Display Settings";

            public const string PayslipDisplaySetupSaveFailed  = "Failed to save Payslip Display Settings";

            #endregion

            #region Create Transaction
            public static readonly string TransactionCreatedSuccessfully = "Create transaction successfully.";
            public static readonly string TransactionCreationFailed = "Failed to create transaction.";
            public static readonly string DuplicateTransaction = "Transaction exists with the same type and description.";

            public static readonly string TransactionUpdatedSuccessfully = "Transaction updated successfully.";
            public static readonly string TransactionUpdateFailed = "Failed to update transaction.";

            #endregion
        }
    }

}
