namespace PalladiumPayroll.Helper.Constants
{
    public static class AppConstants
    {
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
        }
    }

}
