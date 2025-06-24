using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Company;
using PalladiumPayroll.Repositories.User;
using System.Net;
using System.Net.Mail;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        public CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository, EmailService emailService, IConfiguration configuration)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<JsonResult> CreateCompany(CreateCompanyRequest request)
        {
            try
            {
                // Check if email exists
                if (await _userRepository.CheckEmailExist(request.Email))
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.Found,
                        message: ResponseMessages.EmailAlreadyExits,
                        data: string.Empty
                    );
                }

                // Check if company already exists
                if (await _companyRepository.CheckCompanyExist(request.Company))
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.Found,
                        message: ResponseMessages.CompanyAlreadyExists,
                        data: string.Empty
                    );
                }

                // Create company
                long companyId = await _companyRepository.CreateCompany(request);
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.InternalServerError,
                        message: "Error While Creating the Company!!",
                        data: string.Empty
                    );
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

                bool isUserCreated = await _companyRepository.CreateUser(createUserRequest);
                if (!isUserCreated)
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.InternalServerError,
                        message: "Error While Creating User!!",
                        data: string.Empty
                    );
                }

                #region Send Email

                try
                {
                    string subject = "Premium Pay Welcome email";

                    string webUrl = _configuration["Payroll:WebUrl"]!;
                    string loginUrl = "/auth/login";

                    //TODO : add an actual token
                    string token = "khskfhfjhfdskjfh";
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

                    //TODO : change it to actual email
                    mailMessage.To.Add("meetpanchal194@gmail.com");

                    string emailSent = _emailService.SendMail(mailMessage);

                    if (emailSent == ResponseMessages.EmailSentSuccessfully)
                    {
                        return HttpStatusCodeResponse.GenerateResponse(
                            result: true,
                            statusCode: HttpStatusCode.OK,
                            message: ResponseMessages.EmailSentSuccessfully,
                            data: string.Empty
                        );
                    }
                    else
                    {
                        return HttpStatusCodeResponse.GenerateResponse(
                            result: false,
                            statusCode: HttpStatusCode.InternalServerError,
                            message: ResponseMessages.EmailSentFailure,
                            data: string.Empty
                        );
                    }
                }
                catch (Exception)
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.InternalServerError,
                        message: "Internal Server Error!!",
                        data: string.Empty
                    );
                }

                #endregion
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.GenerateResponse(
                    result: false,
                    statusCode: HttpStatusCode.InternalServerError,
                    message: "Internal Server Error!!",
                    data: string.Empty
                );
            }
        }
    }
}
