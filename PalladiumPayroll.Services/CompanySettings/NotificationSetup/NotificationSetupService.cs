using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.CompanySettings
{
    public class NotificationSetupService : INotificationSetupService
    {
        private readonly INotificationSetupRepository _notificationSetupRepository;

        public NotificationSetupService(INotificationSetupRepository notificationSetupRepository)
        {
            _notificationSetupRepository = notificationSetupRepository;
        }

        public async Task<JsonResult> GetNotificationTemplatesByCompanyId(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var templates = await _notificationSetupRepository.GetNotificationTemplatesByCompanyId(companyId);
                if (templates.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(templates, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Notification Templates");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching notification templates: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetNotificationTypes()
        {
            try
            {
                var types = await _notificationSetupRepository.GetNotificationTypes();
                if (types.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(types, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Notification Types");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching notification types: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetEmployeesByCompanyIdForNotification(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var employees = await _notificationSetupRepository.GetEmployeesByCompanyIdForNotification(companyId);
                if (employees.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(employees, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Employees");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching employees: {ex.Message}");
            }
        }

        public async Task<JsonResult> CreateNotificationTemplate(NotificationTemplateRequestDTO request)
        {
            try
            {
                if (request.CompanyId <= 0 || request.NotificationTypeId <= 0 || string.IsNullOrWhiteSpace(request.NotificationTemplateName) ||
                    string.IsNullOrWhiteSpace(request.Subject))
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var templateId = await _notificationSetupRepository.CreateNotificationTemplate(request);
                return HttpStatusCodeResponse.SuccessResponse(templateId, "Notification template created successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error creating notification template: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetNotificationTemplateById(int notificationTemplateId)
        {
            try
            {
                if (notificationTemplateId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var template = await _notificationSetupRepository.GetNotificationTemplateById(notificationTemplateId);
                if (template != null)
                {
                    return HttpStatusCodeResponse.SuccessResponse(template, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Notification Template");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching notification template: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetEmployeesByNotificationTemplateId(int notificationTemplateId)
        {
            try
            {
                if (notificationTemplateId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var employeeIds = await _notificationSetupRepository.GetEmployeesByNotificationTemplateId(notificationTemplateId);
                return HttpStatusCodeResponse.SuccessResponse(employeeIds, ResponseMessages.DataFetchSuccess);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching employees: {ex.Message}");
            }
        }

        public async Task<JsonResult> UpdateNotificationTemplate(NotificationTemplateRequestDTO request)
        {
            try
            {
                if (request.NotificationTemplateId <= 0 || request.CompanyId <= 0 || request.NotificationTypeId <= 0 ||
                    string.IsNullOrWhiteSpace(request.NotificationTemplateName) || string.IsNullOrWhiteSpace(request.Subject))
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var templateId = await _notificationSetupRepository.UpdateNotificationTemplate(request);
                return HttpStatusCodeResponse.SuccessResponse(templateId, "Notification template updated successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error updating notification template: {ex.Message}");
            }
        }

        public async Task<JsonResult> DeleteNotificationTemplate(int notificationTemplateId)
        {
            try
            {
                if (notificationTemplateId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var templateId = await _notificationSetupRepository.DeleteNotificationTemplate(notificationTemplateId);
                return HttpStatusCodeResponse.SuccessResponse(templateId, "Notification template deleted successfully.");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error deleting notification template: {ex.Message}");
            }
        }
    }
}