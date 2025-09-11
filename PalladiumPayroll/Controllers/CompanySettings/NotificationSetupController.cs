using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationSetupController : ControllerBase
    {
        private readonly INotificationSetupService _notificationSetupService;

        public NotificationSetupController(INotificationSetupService notificationSetupService)
        {
            _notificationSetupService = notificationSetupService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetNotificationTemplatesByCompanyId(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
                }
                return await _notificationSetupService.GetNotificationTemplatesByCompanyId(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.NotificationTemplate));
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetNotificationTypes()
        {
            try
            {
                return await _notificationSetupService.GetNotificationTypes();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.Notification + " Types"));
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeesByCompanyIdForNotification(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
                }
                return await _notificationSetupService.GetEmployeesByCompanyIdForNotification(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.Employee + " for " + ResponseMessages.NotificationSetup));
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateNotificationTemplate([FromBody] NotificationTemplateRequestDTO request)
        {
            try
            {
                return await _notificationSetupService.CreateNotificationTemplate(request);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                 string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.NotificationTemplate)
             );
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetNotificationTemplateById(int notificationTemplateId)
        {
            try
            {
                if (notificationTemplateId <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InvalidTemplateId);
                }
                return await _notificationSetupService.GetNotificationTemplateById(notificationTemplateId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.NotificationTemplate));
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeesByNotificationTemplateId(int notificationTemplateId)
        {
            try
            {
                 if (notificationTemplateId <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InvalidTemplateId);
                }

                return await _notificationSetupService.GetEmployeesByNotificationTemplateId(notificationTemplateId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.Employee));
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateNotificationTemplate([FromBody] NotificationTemplateRequestDTO request)
        {
            try
            {
                JsonResult? res = await _notificationSetupService.UpdateNotificationTemplate(request);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.NotificationTemplate));
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteNotificationTemplate(int notificationTemplateId)
        {
            try
            {
                 if (notificationTemplateId <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InvalidTemplateId);
                }
                JsonResult? res = await _notificationSetupService.DeleteNotificationTemplate(notificationTemplateId);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.NotificationTemplate)
            );
            }
        }
    }
}