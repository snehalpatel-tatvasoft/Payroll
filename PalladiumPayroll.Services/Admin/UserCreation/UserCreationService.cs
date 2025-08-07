using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Admin;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Admin;
using Microsoft.AspNetCore.Identity;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Admin
{
    public class UserCreationService : IUserCreationService
    {
        private readonly IUserCreationRepository _userCreationRepository;

        public UserCreationService(IUserCreationRepository userCreationRepository)
        {
            _userCreationRepository = userCreationRepository;
        }

        public async Task<JsonResult> CreateUser(UserCreationRequestDTO request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.UserName) || 
                    string.IsNullOrWhiteSpace(request.SurName) || 
                    string.IsNullOrWhiteSpace(request.Email) || 
                    string.IsNullOrWhiteSpace(request.Password) || 
                    request.CompanyId <= 0 || 
                    request.AccessRoleId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                // Hash password
                string passwordHash = new PasswordHasher<object>().HashPassword(null, request.Password);

                UserCreationRequestDTO createUserRequest = new UserCreationRequestDTO
                {
                    UserName = request.UserName,
                    SurName = request.SurName,
                    Email = request.Email,
                    AccessRoleId = request.AccessRoleId,
                    Password = request.Password,
                    PasswordHash = passwordHash,
                    ContactNo = request.ContactNo,
                    CompanyId = request.CompanyId
                };

                var result = await _userCreationRepository.CreateUser(createUserRequest);
                if (result == -1)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("User with this email already exists for the company.");
                }
                if (result == 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Failed to create user.");
                }

                return HttpStatusCodeResponse.SuccessResponse(result, "User created successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error creating user: {ex.Message}");
            }
        }

        public async Task<JsonResult> UpdateUser(UserCreationRequestDTO request, Guid id)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.UserName) || 
                    string.IsNullOrWhiteSpace(request.SurName) || 
                    string.IsNullOrWhiteSpace(request.Email) || 
                    request.CompanyId <= 0 || 
                    request.AccessRoleId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                // Hash password 
                string passwordHash = string.IsNullOrEmpty(request.Password) 
                    ? request.PasswordHash 
                    : new PasswordHasher<object>().HashPassword(null, request.Password);

                UserCreationRequestDTO updateUserRequest = new UserCreationRequestDTO
                {
                    UserName = request.UserName,
                    SurName = request.SurName,
                    Email = request.Email,
                    AccessRoleId = request.AccessRoleId,
                    Password = request.Password,
                    PasswordHash = passwordHash,
                    ContactNo = request.ContactNo,
                    CompanyId = request.CompanyId
                };

                var result = await _userCreationRepository.UpdateUser(updateUserRequest, id);
                if (result == -1)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("User with this email already exists for the company.");
                }
                if (result == 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse("User not found or already deleted.");
                }

                return HttpStatusCodeResponse.SuccessResponse(result, "User updated successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error updating user: {ex.Message}");
            }
        }

        public async Task<JsonResult> DeleteUser(Guid id, long companyId)
        {
            try
            {
                if (id == Guid.Empty || companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var result = await _userCreationRepository.DeleteUser(id, companyId);
                if (result == 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse("User not found or already deleted.");
                }

                return HttpStatusCodeResponse.SuccessResponse(result, "User deleted successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error deleting user: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetUsersByCompanyId(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var users = await _userCreationRepository.GetUsersByCompanyId(companyId);
                if (users.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(users, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Users");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching users: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetAccessRolesNamesByCompanyId(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var accessRoles = await _userCreationRepository.GetAccessRolesNamesByCompanyId(companyId);
                if (accessRoles.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(accessRoles, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Access Roles");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching access roles: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetUserById(Guid id, long companyId)
        {
            try
            {
                if (id == Guid.Empty || companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var user = await _userCreationRepository.GetUserById(id, companyId);
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    return HttpStatusCodeResponse.SuccessResponse(user, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("User");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching user: {ex.Message}");
            }
        }


        public async Task<JsonResult> GetUserFunctionalityById(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty )
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var accessRights = await _userCreationRepository.GetUserFunctionalityById(userId);

                return HttpStatusCodeResponse.SuccessResponse(accessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching user functionality: {ex.Message}");
            }
        }

        public async Task<JsonResult> SaveUserFunctionalityAccessRights(List<SaveUserFunctionalityAccessRightsDTO> request)
        {
            try
            {
                if (request == null || !request.Any())
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                bool isSaved = await _userCreationRepository.SaveUserFunctionalityAccessRights(request);
                return isSaved
                    ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Saved))
                    : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRights);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error saving user functionality access rights: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetUserPayFrequenciesById(Guid userId, long companyId)
        {
            try
            {
                if (userId == Guid.Empty || companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var accessRights = await _userCreationRepository.GetUserPayFrequenciesById(userId, companyId);

                return HttpStatusCodeResponse.SuccessResponse(accessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching user pay frequencies: {ex.Message}");
            }
        }

        public async Task<JsonResult> SaveUserPayFrequenciesAccessRights(List<SaveUserPayFrequencyAccessRightsDTO> request)
        {
            try
            {
                if (request == null || !request.Any())
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                bool isSaved = await _userCreationRepository.SaveUserPayFrequenciesAccessRights(request);
                return isSaved
                    ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Saved))
                    : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRights);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error saving user pay frequency access rights: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetUserFunctionalityByIdForTransactionFunctions(Guid userId, long companyId)
        {
            try
            {
                if (userId == Guid.Empty || companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var accessRights = await _userCreationRepository.GetUserFunctionalityByIdForTransactionFunctions(userId, companyId);

                return HttpStatusCodeResponse.SuccessResponse(accessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching user transaction function access rights: {ex.Message}");
            }
        }
    }
}