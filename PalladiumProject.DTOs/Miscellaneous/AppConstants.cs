
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
            public static readonly string CompanyInfo = "Company Information";
            public static readonly string CompanyRepresentativeInfo = "Company Representative";
            public const string CompanyAlreadyExists = "Company Already Exists";
            public const string CompanyRegisteredSuccessfully = "Company Registered Successfully";
            public const string ErrorCreatingCompany = "Error While Creating Company!!";

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
        }
    }

}
