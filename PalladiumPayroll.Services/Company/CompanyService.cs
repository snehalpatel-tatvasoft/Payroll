using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Company;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.DTOs.Miscellaneous.Constants;
using PalladiumPayroll.Helper;
using PalladiumPayroll.Helper.ImportExport;
using PalladiumPayroll.Helper.JWTToken;
using PalladiumPayroll.Repositories.Company;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Claims;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly EmailService _emailService;
        private readonly JwtSettings? _jwtSettings;
        private readonly PayrollWebSetting? _payrollWebSetting;
        public CompanyService(ICompanyRepository companyRepository, EmailService emailService, IConfiguration configuration)
        {
            _companyRepository = companyRepository;
            _emailService = emailService;
            _jwtSettings = AppSettingsConfig.GetSection<JwtSettings>(configuration, "Jwt");
            _payrollWebSetting = AppSettingsConfig.GetSection<PayrollWebSetting>(configuration, "Payroll");
        }

        public async Task<JsonResult> CompanyCreation(CompanyModels model)
        {
            return await _companyRepository.CompanyCreation(model);
        }

        public async Task<JsonResult> CheckCompanyExist(CheckCompanyExistModel reqModel)
        {
            bool isExist = await _companyRepository.CheckCompanyExist(reqModel);
            if (isExist)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.AlreadyExist, ResponseMessages.Company));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Valid, ResponseMessages.Company));
        }

        public async Task<JsonResult> CreateCompany(CreateCompanyRequest request)
        {
            // Check if company already exists
            if (await _companyRepository.CheckCompanyExist(request.Company))
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.CompanyAlreadyExists);
            }

            // Create company
            long companyId = await _companyRepository.CreateCompany(request);
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.ErrorCreatingCompany);
            }

            // Hash password and create user
            string passwordHash = new PasswordHasher<object>().HashPassword(user: string.Empty, request.Password);

            CreateUserRequestDto? createUserRequest = new CreateUserRequestDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = SecurityHandler.Encrypt(request.Password),
                PasswordHash = passwordHash,
                ContactNo = request.ContactNo,
                CompanyId = (int)companyId
            };

            Guid userId = await _companyRepository.CreateUser(createUserRequest);
            if (userId == Guid.Empty)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.ErrorCreatingUser);
            }

            #region Send Email

            string subject = "Premium Pay Welcome email";

            string webUrl = _payrollWebSetting?.WebUrl!;

            string token = JwtTokenHelper.GenerateToken(
                    [new Claim(JWTClaimTypes.UserId, userId.ToString())],
                    DateTime.Now.AddMinutes(AuthTokenExpiryInMinutes),
                    _jwtSettings?.Key!,
                    _jwtSettings?.Issuer!,
                    _jwtSettings?.Audience!
                );

            // Append the token directly to the URL
            string finalUrl = $"{webUrl}/auth/login?token={token}";

            string templatePath = FileHandler.EmailTemplatePath("WelcomeEmail.html");
            string bodyTemplate = await FileHandler.ReadFileContent(templatePath);

            string emailBody = bodyTemplate
                            .Replace("{UserName}", request.FirstName ?? "User")
                            .Replace("{LoginUrl}", $"<a href='{finalUrl}' target='_blank'>Click here</a>");

            MailMessage mailMessage = new MailMessage
            {
                Body = emailBody,
                Subject = subject,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(request.Email);

            bool isEmailSent = await _emailService.SendMail(mailMessage);

            if (isEmailSent)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.EmailSentSuccessfully);
            }
            else
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmailSentFailure);
            }

            #endregion
        }

        public async Task<JsonResult> AddNewBank(BankModel bankModel)
        {
            bool isAdded = await _companyRepository.AddNewBank(bankModel);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Bank", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.AlreadyExist, "Branch"));
        }

        public async Task<List<DropDownViewModel>> GetCompanyWithSubCompany(int companyId, string userId)
        {
            return await _companyRepository.GetCompanyWithSubCompany(companyId, userId);
        }

        public async Task<JsonResult> SetActiveCompanyId(int companyId)
        {
            bool isAdded = await _companyRepository.SetActiveCompanyId(companyId);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Bank", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.AlreadyExist, "Branch"));
        }

        public async Task<List<CompanyInfo>> GetCompanyInformation(int companyId)
        {
            return await _companyRepository.GetCompanyInformation(companyId);
        }

        public async Task<List<GLSetup>> GetCompanyGLInfo(int companyId)
        {
            return await _companyRepository.GetCompanyGLInfo(companyId);
        }
        public async Task<JsonResult> UpdateCompanyInformation(CompanyInfo companyInfo)
        {
            bool isAdded = await _companyRepository.UpdateCompanyInformation(companyInfo);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyInfo, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<List<CompanyRepresentative>> GetCompanyRepresentativeInfo(int companyId)
        {
            return await _companyRepository.GetCompanyRepresentativeInfo(companyId);
        }

        public async Task<JsonResult> UpdateCompanyRepresentativeInfo(CompanyRepresentative companyRepresentativeInfo)
        {
            bool isAdded = await _companyRepository.UpdateCompanyRepresentativeInfo(companyRepresentativeInfo);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyRepresentativeInfo, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<List<CompanyBankAccount>> GetBankDetailsInfo(int companyId)
        {
            return await _companyRepository.GetBankDetailsInfo(companyId);
        }

        public async Task<JsonResult> UpdateBankDetailsInfo(CompanyBankAccount companyBankAccount)
        {
            bool isAdded = await _companyRepository.UpdateBankDetailsInfo(companyBankAccount);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyBankDetails, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<List<CompanyPayrollCycle>> GetPayrollCycleInfo(int companyId, int taxYearId)
        {
            return await _companyRepository.GetPayrollCycleInfo(companyId, taxYearId);
        }

        public async Task<List<CompanyCoidaSetup>> GetCOIDASetupInfo(int companyId, int yearId)
        {
            return await _companyRepository.GetCOIDASetupInfo(companyId, yearId);
        }

        public async Task<List<PayrollMedicalAidList>> GetMedicalAidFundInfo(int companyId)
        {
            return await _companyRepository.GetMedicalAidFundInfo(companyId);
        }

        public async Task<List<PayrollBenefitFundList>> GetCompanyBenefitFundInfo(int companyId)
        {
            return await _companyRepository.GetCompanyBenefitFundInfo(companyId);
        }

        public async Task<JsonResult> UpsertPayrollCycleInfo(CompanyPayrollCycle companyPayrollCycle)
        {
            bool isAdded = await _companyRepository.UpsertPayrollCycleInfo(companyPayrollCycle);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyBankDetails, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<JsonResult> GetProcessCyclePeriodInfo(int payrollId)
        {
            List<CyelePeriod> cycleList = await _companyRepository.GetProcessCyclePeriodInfo(payrollId);
            return HttpStatusCodeResponse.SuccessResponse(cycleList, string.Format(ResponseMessages.Success, "Cycle Periods", ActionType.Retrieved));
        }

        public async Task<JsonResult> UpsertCompanyBenefitFund(PayrollBenefitFundList payrollBenefitFundList)
        {
            bool isAdded = await _companyRepository.UpsertCompanyBenefitFund(payrollBenefitFundList);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyBankDetails, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<JsonResult> UpsertCOIDASetupInfo(CompanyCoidaSetup companyCoidaSetup)
        {
            bool isAdded = await _companyRepository.UpsertCOIDASetupInfo(companyCoidaSetup);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyBankDetails, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<JsonResult> AddMedicalAidFundInfo(PayrollMedicalAidList payrollMedicalAidList)
        {
            bool isAdded = await _companyRepository.AddMedicalAidFundInfo(payrollMedicalAidList);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyBankDetails, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<JsonResult> DeletePayrollCycleInfo(int cycleId)
        {
            bool isDeleted = await _companyRepository.DeletePayrollCycleInfo(cycleId);
            if (isDeleted)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyBankDetails, ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<JsonResult> DeleteMedicalAidFund(int fundId)
        {
            bool isDeleted = await _companyRepository.DeleteMedicalAidFund(fundId);
            if (isDeleted)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyBankDetails, ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<JsonResult> DeleteCompanyBenefitFund(int fundId)
        {
            bool isDeleted = await _companyRepository.DeleteCompanyBenefitFund(fundId);
            if (isDeleted)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CompanyBankDetails, ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<JsonResult> GetGLSetup(DBConnectionModel dbConnectionModel)
        {
            GLConnRes glSetup = new();
            bool IsDbConnection = await _companyRepository.CheckGLDBConnection(dbConnectionModel);
            glSetup.IsDbConnection = IsDbConnection;
            glSetup.ConnectionMessage = !IsDbConnection ? ResponseMessages.GLSetupError : ResponseMessages.GLSetupSuccess;

            if (IsDbConnection)
            {
                glSetup.GlAccountList = await _companyRepository.GetGLAccounts(dbConnectionModel);
                glSetup.GlDepartmentList = await _companyRepository.GetGLDepartments(dbConnectionModel);
            }
            return HttpStatusCodeResponse.SuccessResponse(glSetup, string.Format(ResponseMessages.Success, "GL Account", ActionType.Retrieved));
        }

        public async Task<List<TransactionListForCompany>> GetTransactionList(long companyId)
        {
            return await _companyRepository.GetTransactionList(companyId);
        }

        public async Task<byte[]> ExportGLTransactionList(long companyId)
        {
            var data = await GetTransactionList(companyId);
            var transactionDT = new DataTable();
            transactionDT.Columns.Add("Description", typeof(string));
            transactionDT.Columns.Add("Debit Account Number", typeof(long));
            transactionDT.Columns.Add("Credit Account Number", typeof(long));
            transactionDT.Columns.Add("Contra Account Number", typeof(long));
            foreach (var item in data)
            {
                transactionDT.Rows.Add(item.Description, item.DebitAccountNumber, item.CreditAccountNumber, item.ContraAccountNumber);
            }
            return ExcelHelper.ExportToExcel(new Dictionary<string, DataTable> { { "Sheet 1", transactionDT } });
        }

        public async Task<JsonResult> ImportGLTransaction(ImportFileModel requestData)
        {
            DataColumn[] sheetColumn =
                [
                    new DataColumn("Description", typeof(string)),
                    new DataColumn("Type", typeof(string)),
                    new DataColumn("DebitAccountNumber", typeof(string)),
                    new DataColumn("CreditAccountNumber", typeof(string)),
                    new DataColumn("ContraAccountNumber", typeof(string)),
                ];
            var transactionDT = ExcelHelper.ImportFromExcel(requestData.File, true, sheetColumn);
            var res = await _companyRepository.ImportGLTransaction(transactionDT, requestData.Id);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Imported));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TransactionUpdateFailed);

        }

        public async Task<bool> SaveGlAccountNumber(TransactionListForCompany model)
        {
            return await _companyRepository.SaveGlAccountNumber(model);
        }

        public async Task<JsonResult> UpsertEmploymentEquityInfo(EmploymentEquityInformation employmentEquityInformation)
        {
            bool isAdded = await _companyRepository.UpsertEmploymentEquityInfo(employmentEquityInformation);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmploymentEquityInformation, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<List<EmploymentEquityInformation>> GetEmploymentEquityInfo(int companyId)
        {
            return await _companyRepository.GetEmploymentEquityInfo(companyId);
        }

        public async Task<IndustryCouncil> GetIndustrialCouncil(int companyId)
        {

            IndustryCouncil industryCouncil = await _companyRepository.GetIndustrialCouncil(companyId);
            industryCouncil.MIBFACouncil = new MIBFACouncil()
            {
                FirmNumber = industryCouncil.MIBFACouncilSetupDetail.FirmNumber,
                TradeUnionCode = industryCouncil.MIBFACouncilSetupDetail.TradeUnionCode,
                CouncilLevyReturnReport = GetIntegerList(industryCouncil.MIBFACouncilSetupDetail.CouncilLevyReturnTransactionsIds),
                SickFundReport = GetIntegerList(industryCouncil.MIBFACouncilSetupDetail.SickFundTransactionsIds),
                ProvidentFundReport = GetIntegerList(industryCouncil.MIBFACouncilSetupDetail.ProvidentFundTransactionsIds),
                PensionFundReport = GetIntegerList(industryCouncil.MIBFACouncilSetupDetail.PensionFundTransactionsIds),
                MIBFAReport = GetIntegerList(industryCouncil.MIBFACouncilSetupDetail.MIBFATransactionsIds),

            };
            return industryCouncil;

        }

        public async Task<JsonResult> SaveIndustrialCouncil(IndustryCouncil idustryCouncil)
        {
            bool isAdded = await _companyRepository.SaveIndustrialCouncil(idustryCouncil);
            if (isAdded)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.IndustrialCouncil, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<ActionResult> GetMIBFADropdown(int companyId)
        {
            List<TransactionDropdown> list = await _companyRepository.GetMIBFADropdown(companyId);
            MIBFAOptions mibfaOptions = new MIBFAOptions();
            mibfaOptions.MIBFAReportOptions = list.Select(mibfaMapper).ToList();
            mibfaOptions.SickFundReportOptions = list.Select(mibfaMapper).ToList();
            mibfaOptions.PensionFundReportOptions = list.Select(mibfaMapper).ToList();
            mibfaOptions.ProvidentFundReportOptions = list.Select(mibfaMapper).ToList();
            mibfaOptions.CouncilLevyReturnReportOptions = list.Select(mibfaMapper).ToList();
            return HttpStatusCodeResponse.SuccessResponse(mibfaOptions, string.Format(ResponseMessages.Success, "MIBFA", ActionType.Retrieved));
        }

        private List<Int32> GetIntegerList(string s)
        {
            if (string.IsNullOrEmpty(s))
                return new List<Int32>();
            List<Int32> list;
            list = s.Split(',').Select(a => Int32.Parse(a)).ToList();
            return list;
        }

        private static readonly Func<TransactionDropdown, MIBFASelectOptions> mibfaMapper = (input) =>
        {
            return new MIBFASelectOptions()
            {
                Id = input.PayrollProcessId,
                Key = input.Description,
                Value = input.Description
            };
        };
    }
}
