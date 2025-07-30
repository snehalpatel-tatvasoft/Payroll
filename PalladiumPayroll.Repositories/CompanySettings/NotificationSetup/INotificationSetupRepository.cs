using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Repositories.CompanySettings
{
    public interface INotificationSetupRepository
    {
        Task<List<NotificationTemplateResponseDTO>> GetNotificationTemplatesByCompanyId(long companyId);
        Task<List<NotificationTypeResponseDTO>> GetNotificationTypes();
        Task<List<EmployeeResponseDTO>> GetEmployeesByCompanyIdForNotification(long companyId);
        Task<int> CreateNotificationTemplate(NotificationTemplateRequestDTO request);
        Task<NotificationTemplateResponseDTO> GetNotificationTemplateById(int notificationTemplateId);
        Task<List<long>> GetEmployeesByNotificationTemplateId(int notificationTemplateId);
        Task<int> UpdateNotificationTemplate(NotificationTemplateRequestDTO request);
        Task<int> DeleteNotificationTemplate(int notificationTemplateId);
    }
}