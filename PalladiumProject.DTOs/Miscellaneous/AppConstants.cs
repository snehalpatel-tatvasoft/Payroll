namespace PalladiumPayroll.Helper.Constants
{
    public static class AppConstants
    {
        public const int DefaultPageSize = 999999999;

        public const int AuthTokenExpiryInMinutes = 60;

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
            public static readonly string UnexpectedError = "Unexpected error occurred";
            public static readonly string InvalidOrMissingRequestParameters = "Invalid or missing request parameters";
            public static readonly string UnAuthorized = "UnAuthorized Access !!";

            #endregion

            #region Employee

            public static readonly string Employee = "Employee";

            #endregion

            #region Company

            public static readonly string Company = "Company";
            public const string CompanyAlreadyExists = "Company Already Exists";
            public const string CompanyRegisteredSuccessfully = "Company Registered Successfully";

            #endregion

            #region User

            public static readonly string User = "User";
            public const string UserNotFound = "We couldn't find an account associated with this email address!";
            public const string EmailAlreadyExits = "Email Already Exists";

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
            
            #endregion

            #region Dashboard

            public static readonly string Dashboard = "Dashboard data";

            #endregion
        }
    }

}
