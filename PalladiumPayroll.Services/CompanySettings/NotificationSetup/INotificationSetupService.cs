using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

namespace PalladiumPayroll.Services.CompanySettings
{
    public interface INotificationSetupService
    {
        Task<JsonResult> GetNotificationTemplatesByCompanyId(long companyId);
        Task<JsonResult> GetEmployeesByCompanyIdForNotification(long companyId);
        Task<JsonResult> GetNotificationTypes();
        Task<JsonResult> CreateNotificationTemplate(NotificationTemplateRequestDTO request);
        Task<JsonResult> GetNotificationTemplateById(int notificationTemplateId);
        Task<JsonResult> GetEmployeesByNotificationTemplateId(int notificationTemplateId);
        Task<JsonResult> UpdateNotificationTemplate(NotificationTemplateRequestDTO request);
        Task<JsonResult> DeleteNotificationTemplate(int notificationTemplateId);
    }
}