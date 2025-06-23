using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Company;
using PalladiumPayroll.Repositories.User;
using System.Net;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        public CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
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

                // Final success response
                return HttpStatusCodeResponse.GenerateResponse(
                    result: true,
                    statusCode: HttpStatusCode.OK,
                    message: ResponseMessages.CompanyRegisteredSuccessfully,
                    data: string.Empty
                );
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
