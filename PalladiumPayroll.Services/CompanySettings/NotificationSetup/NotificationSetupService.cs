using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

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
            List<NotificationTemplateResponseDTO> templates = await _notificationSetupRepository.GetNotificationTemplatesByCompanyId(companyId);

            return HttpStatusCodeResponse.SuccessResponse(templates, string.Format(ResponseMessages.Success, ResponseMessages.NotificationTemplate, ActionType.Retrieved));
        }

        public async Task<JsonResult> GetNotificationTypes()
        {
            List<NotificationTypeResponseDTO>? types = await _notificationSetupRepository.GetNotificationTypes();

            return HttpStatusCodeResponse.SuccessResponse(types, string.Format(ResponseMessages.Success, ResponseMessages.Notification + " Types", ActionType.Retrieved));
        }

        public async Task<JsonResult> GetEmployeesByCompanyIdForNotification(long companyId)
        {
            List<EmployeeResponseDTO>? employees = await _notificationSetupRepository.GetEmployeesByCompanyIdForNotification(companyId);

            return HttpStatusCodeResponse.SuccessResponse(employees, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieved));
        }

        public async Task<JsonResult> CreateNotificationTemplate(NotificationTemplateRequestDTO request)
        {
            if (request.CompanyId <= 0 || request.NotificationTypeId <= 0 || string.IsNullOrWhiteSpace(request.NotificationTemplateName) ||
                    string.IsNullOrWhiteSpace(request.Subject))
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }

            int templateId = await _notificationSetupRepository.CreateNotificationTemplate(request);

            if (templateId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToCreateNotificationTemplate);
            }
            return HttpStatusCodeResponse.SuccessResponse(templateId, string.Format(ResponseMessages.Success, ResponseMessages.NotificationTemplate, ActionType.Created));
        }

        public async Task<JsonResult> GetNotificationTemplateById(int notificationTemplateId)
        {
            NotificationTemplateResponseDTO? template = await _notificationSetupRepository.GetNotificationTemplateById(notificationTemplateId);
            if (template != null)
            {
                return HttpStatusCodeResponse.SuccessResponse(template, string.Format(ResponseMessages.Success, ResponseMessages.NotificationTemplate, ActionType.Retrieved));
            }
            return HttpStatusCodeResponse.NotFoundResponse("Notification Template");
        }

        public async Task<JsonResult> GetEmployeesByNotificationTemplateId(int notificationTemplateId)
        {
            List<long>? employeeIds = await _notificationSetupRepository.GetEmployeesByNotificationTemplateId(notificationTemplateId);

            return HttpStatusCodeResponse.SuccessResponse(employeeIds, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieved));
        }

        public async Task<JsonResult> UpdateNotificationTemplate(NotificationTemplateRequestDTO request)
        {
            if (request.NotificationTemplateId <= 0 || request.CompanyId <= 0 || request.NotificationTypeId <= 0 ||
                string.IsNullOrWhiteSpace(request.NotificationTemplateName) || string.IsNullOrWhiteSpace(request.Subject))
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }

            int templateId = await _notificationSetupRepository.UpdateNotificationTemplate(request);

            if (templateId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToCreateNotificationTemplate);
            }
            return HttpStatusCodeResponse.SuccessResponse(templateId, string.Format(ResponseMessages.Success, ResponseMessages.NotificationTemplate, ActionType.Updated));
        }

        public async Task<JsonResult> DeleteNotificationTemplate(int notificationTemplateId)
        {
            int templateId = await _notificationSetupRepository.DeleteNotificationTemplate(notificationTemplateId);

            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.NotificationTemplate, ActionType.Deleted));

        }
    }
}
