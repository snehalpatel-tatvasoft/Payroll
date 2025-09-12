namespace PalladiumPayroll.Helper.Constants
{
    public static class AppConstants
    {
        public const int DefaultPageSize = 10;

        public const int AuthTokenExpiryInMinutes = 10;

        public const int RefreshTokenExpiryInDays = 7;

        public const int ResetPasswordTokenExpiryInDays = 1;

        public const int EmailVerificationTokenLength = 100;

        public const int ResetPasswordTokenLength = 100;

        public const int PageNumber = 1;

        public const string DefaultConnectionString = "Data Source={0}; initial catalog={1}; User ID={2}; Password={3}; TrustServerCertificate=True;";
        public const string DefaultSQLQuery = "SELECT {0} FROM [dbo].{1}";

        public const string SortAsc = "ASC";
        public const string SortDesc = "DESC";

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
            public static readonly string ExceptionMessage = "An Error Occurred While {0} {1}";
            public static readonly string EmptyFile = "File is empty !!";
            public static readonly string InavalidFile = "File format is invalid !";
            public static readonly string Failed = "Failed to {0} {1}";

            #endregion

            #region Employee

            public static readonly string Employee = "Employee";
            public static readonly string CasualWageInformation = "Employee Casual Wage Information";
            public static readonly string DirectiveInformation = "Employee  Directive Information";
            public static readonly string TaxInformation = "Employee  Tax Information";
            public static readonly string LeaveInformation = "Employee  Leave Information";




            #endregion

            #region Company

            public static readonly string Company = "Company";
            public static readonly string CompanyInfo = "Company Information";
            public static readonly string CompanyRepresentativeInfo = "Company Representative";
            public static readonly string CompanyBankDetails = "Company Bank Details";
            public static readonly string EmploymentEquityInformation = "Employment Equity Information";
            public const string CompanyAlreadyExists = "Company Already Exists";
            public const string CompanyRegisteredSuccessfully = "Company Registered Successfully";
            public const string ErrorCreatingCompany = "Error While Creating Company!!";
            public const string GLSetupError = "Error while connecting GL Database!!";
            public const string GLSetupSuccess = "GL database connected successfully!!";

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
            public const string InvalidToken = "Invalid token";
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
            public const string ResetPasswordEmailSentSuccesfully = "Reset password email sent. Please click on the link to reset new password.";

            #endregion

            #region Application Admin
            public static readonly string AppAdminDashboard = "Application Admin Dashboard Data";


            #endregion

            #region Dashboard

            public static readonly string Dashboard = "Dashboard data";

            #endregion

            #region Designation
            public static readonly string Designations = "Designations";
            public const string DesignationsCreationFailed = "Failed to create designation";
            public const string DataFetchSuccess = "Data fetched successfully";
            public const string DesignationsDeleteFailed = "Failed to deleted designation";
            public const string DesignationsUpdateFailed = "Failed to update designation";
            public const string DesignationDuplicate = "Designation with the same Name and Code already exists.";
            public const string DesignationsImportedSuccessfully = "Designations imported successfully.";

            public static readonly string ErrorDeletingDesignations = "Error deleting Designation.";


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
            public static readonly string Department = "Department";
            public static readonly string CheckDuplicateDepartment = "This department name already exists. Please choose a different one.";
            public static readonly string UnableToCreateDepartment = "Unable to create department.";
            public static readonly string ErrorUpdatingDepartment = "Error updating department.";
            public static readonly string ErrorDeletingDepartment = "Error deleting department.";

            #endregion


            #region Employee Code

            public static readonly string EmployeeCode = "Employee Code";

            public const string EmployeeCodeSaveFailed = "Failed to save employee code";

            #endregion


            #region Payslip Display Setup

            public static readonly string PayslipDisplaySetup = "Payslip Display Settings";

            public const string PayslipDisplaySetupSaveFailed = "Failed to save Payslip Display Settings";

            #endregion

            #region Create Transaction
            public static readonly string Transaction = "Transaction";
            public static readonly string CreateTransaction = "Create Transaction";
            public static readonly string TransactionCreationFailed = "Failed to create transaction.";
            public static readonly string DuplicateTransaction = "Transaction exists with the same type and description.";
            public static readonly string TransactionUpdateFailed = "Failed to update transaction.";
            public static readonly string TransactionImportFailed = "Error occurred while importing the data";
            public static readonly string TransactionDeleteFailed = "Failed to delete Transaction.";


            #endregion

            #region Employee Grievances

            public static readonly string EmployeeGrievances = "Employee Grievances";

            public static readonly string NatureOfGrievances = "Nature of Grievances";
            public const string EmployeeGrievanceSaveFailed = "Failed to save Employee Grievances";
            public const string InvalidEmployeeGrievanceId = "Employee Grievance Id is Invalid.";
            public const string EmployeeGrievanceNotFound = "Employee Grievance was not found.";

            #endregion


            #region Employees Loan
            public static readonly string EmployeeLoan = "Employee Loan";
            public static readonly string LoanNotFound = "Employee Loan not Found.";
            public static readonly string LoanPausedSuccessfully = "Loan paused successfully.";
            public static readonly string LoanPausedFailed = "Failed to pause the loan.";
            public static readonly string LoanCreatedSuccessfully = "Loan created successfully.";
            public static readonly string LoanCreationFailed = "Loan creation failed.";
            public static readonly string LoanUpdatedSuccessfully = "Loan updated successfully.";
            public static readonly string LoanUpdatedFailed = "Loan update failed.";
            public static readonly string LoanDataFetchedSuccessfully = "Loan data fetched successfully.";
            public static readonly string LoanPaidSuccessfully = "Full Loan paid successfully.";
            public static readonly string LoanPaidFailed = "Failed to pay full loan.";

            #endregion


            #region Employee Promotions


            public static readonly string EmployeePromotions = "Employee Promotions";
            public const string EmployeePromotionSaveFailed = "Failed to save Employee Promotion";

            public static readonly string NatureOfPromotions = "Nature of Promotions";
            public const string InvalidEmployeePromotionId = "Employee Promotion Id is Invalid.";
            public const string EmployeePromotionNotFound = "Employee Promotion was not found.";

            #endregion

            #region Employee Training

            public static readonly string EmployeeTraining = "Employee Training";
            public const string EmployeeTrainingSaveFailed = "Failed to save Employee Training";
            public const string InvalidEmployeeTrainingnId = "Employee Training Id is Invalid.";
            public const string EmployeeTrainingNotFound = "Employee Training was not found.";

            #endregion

            #region Employee Transfer

            public static readonly string EmployeeTransfer = "Employee Transfer";
            public static readonly string EmployeeTransferCreatedSuccessfully = "Employee Transfer Created Successfully";
            public static readonly string EmployeeTransferCreationFailed = "Employee Transfer Creation Failed";
            public static readonly string EmployeeOrCompanyIdInvalid = "EmployeeId Or CompanyId is Invalid";
            public static readonly string EmployeeNotFound = "Employee Not Found";
            public static readonly string InvalidEmployeeOrCompanyId = "Invalid Employee Or CompanyId";

            #endregion


            #region access rights

            public static readonly string AccessRole = "Access Role";
            public const string AcceessRoleAlreadyExists = "Access Role with this name is alreasy exists.";
            public const string UnableToSaveAccessRole = "Unable to save Access Role";
            public const string AccessRoleNotFound = "Access Role could not be deleted or was not found.";

            public const string InvalidAccessRoleId = "Access Role Id is Invalid.";

            public static readonly string AccessRights = "Access Rights";
            public const string UnableToSaveAccessRights = "Unable to save Access Rights";

            #endregion


            #region savings and garnishee

            public static readonly string Savings = "Savings";
            public static readonly string Garnishee = "Garnishee";
            public static readonly string GarnisheeSavedFailed = "Failed to save Garnishee.";
            public static readonly string SavingsSavedFailed = "Failed to save Savings.";

            #endregion

            #region Coid Accident
            public static readonly string CoidAccident = "COID accident";
            public static readonly string CoidAccidentFailedDelete = "Failed to delete COID Accident ";
            public static readonly string CoidAccidentUpdatedSuccessfully = "COID Accident updated successfully";
            public static readonly string CoidAccidentCreatedSuccessfully = "COID Accident created successfully";
            public static readonly string CoidAccidentCreateFailed = "COID Accident creation failed";
            public static readonly string CoidAccidentUpdateFailed = "COID Accident update failed";

            #endregion

            #region Data Import
            public static readonly string YearToDateTemplate = "Year To Date Template";
            public static readonly string WorkInformation = "Work Information";
            public static readonly string EmployeeMasterfile = "Employee Masterfile";
            public static readonly string ImportStatus = "Import Status";
            public static readonly string Timesheet = "Employee Timesheet";
            public static readonly string LeaveTakenOn = "Leave TakenOn";
            public static readonly string LeaveTransaction = "Leave Transaction";

            #endregion


            #region Leave Settings
            public static readonly string LeaveSettings = "Leave Settings";
            public static readonly string LeaveSettingsUpdateFailed = "Failed to update Leave Settings.";

            #endregion




            #region Disciplinary Log

            public static readonly string DisciplinaryLog = "Disciplinary Log";
            public const string DisciplinaryLogSaveFailed = "Failed to save Disciplinary Log";
            public const string InvalidDisciplinaryLogId = "Disciplinary Log Id is Invalid.";
            public const string DisciplinaryLogNotFound = "Disciplinary Log was not found.";

            #endregion

            #region Customize Report

            public static readonly string CustomizeReport = "Customize Report";
            public const string InvalidReportId = "Report Id is Invalid.";
            public const string ErrorSavingCustomizeReport = "Error while Saving Customize Report";

            #endregion

            #region Notification Setup

            public static readonly string NotificationSetup = "Notification Setup";
            public static readonly string Notification = "Notification";
            public static readonly string NotificationTemplate = "Notification Template";
            public static readonly string UnableToCreateNotificationTemplate = "Not able to create Notification Template";
            public static readonly string InvalidTemplateId = "Notification Template Id is not valid.";

            #endregion


            #region Timesheet Setup

            public static readonly string TimesheetSetup = "Payroll Timesheet Setup";
            public static readonly string UnableSaveTimesheetSetup = "Unable to save Payroll Timesheet Setup";
            
            #endregion


             #region Password Policy

            public static readonly string PasswordPolicy = "Password Policy";
            public static readonly string PasswordNotFound = "Password Policy is not found.";
            
            #endregion


             #region Employee Self Service

            public static readonly string EmployeeSelfService = "Employee Self Service";
            public static readonly string UserCredential = "User Credentials";
            public static readonly string SecondApprovalEmployees = "Second Approval Employees";
            
            #endregion


            #region Employee Profile
             public static readonly string EmployeeProfile = "Employee Profile";
            #endregion
        }


        public static class ContentTypes
        {
            public const string OctetStream = "application/octet-stream";
            public const string Json = "application/json";
            public const string Xml = "application/xml";
            public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string Pdf = "application/pdf";
            public const string Text = "text/plain";
            public const string Html = "text/html";
            public const string MultipartFormData = "multipart/form-data";
        }
    }

}
