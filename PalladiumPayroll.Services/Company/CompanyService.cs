using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.DTOs.Miscellaneous.Constants;
using PalladiumPayroll.Helper.Constants;
using PalladiumPayroll.Helper.JWTToken;
using PalladiumPayroll.Repositories.Company;
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
        private readonly IConfiguration _configuration;
        public CompanyService(ICompanyRepository companyRepository, EmailService emailService, IConfiguration configuration)
        {
            _companyRepository = companyRepository;
            _emailService = emailService;
            _configuration = configuration;
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
            try
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
                    Password = request.Password,
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

                try
                {
                    string subject = "Premium Pay Welcome email";

                    string webUrl = _configuration["Payroll:WebUrl"]!;
                    string loginUrl = "/auth/login";

                    JWTTokenService jwtService = new JWTTokenService(_configuration);

                    Claim[] claims = { new Claim(JWTClaimTypes.UserId, userId.ToString()) };

                    string token = jwtService.GenerateToken(
                         claims,
                         DateTime.Now.AddMinutes(AppConstants.AuthTokenExpiryInMinutes),
                         _configuration["Jwt:Key"]!
                     );

                    // Append the token directly to the URL
                    string finalUrl = $"{webUrl}{loginUrl}?token={token}";

                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplate", "WelcomeEmail.html");
                    string bodyTemplate = await File.ReadAllTextAsync(templatePath);

                    string emailBody = bodyTemplate
                                    .Replace("{UserName}", request.FirstName ?? "User")
                                    .Replace("{LoginUrl}", $"<a href='{finalUrl}' target='_blank'>Click here</a>");

                    MailMessage mailMessage = new MailMessage
                    {
                        Body = emailBody,
                        Subject = subject,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add("meet.panchal@tatvasoft.com");
                    //mailMessage.To.Add(request.Email);

                    string emailSent = _emailService.SendMail(mailMessage);

                    if (emailSent == ResponseMessages.EmailSentSuccessfully)
                    {
                        return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.EmailSentSuccessfully);
                    }
                    else
                    {
                        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmailSentFailure);
                    }
                }
                catch (Exception)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                #endregion
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
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

        public async Task<List<DropDownViewModel>> GetCompanyWithSubCompany(int companyId)
        {
            return await _companyRepository.GetCompanyWithSubCompany(companyId);
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
    }
}
