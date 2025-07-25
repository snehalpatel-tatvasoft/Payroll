using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using System.Net;

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

        [HttpGet("GetNotificationTemplatesByCompanyId/{companyId}")]
        public async Task<ActionResult> GetNotificationTemplatesByCompanyId(long companyId)
        {
            try
            {
                JsonResult? res = await _notificationSetupService.GetNotificationTemplatesByCompanyId(companyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("GetNotificationTypes")]
        public async Task<ActionResult> GetNotificationTypes()
        {
            try
            {
                JsonResult? res = await _notificationSetupService.GetNotificationTypes();
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("GetEmployeesByCompanyIdForNotification/{companyId}")]
        public async Task<ActionResult> GetEmployeesByCompanyIdForNotification(long companyId)
        {
            try
            {
                JsonResult? res = await _notificationSetupService.GetEmployeesByCompanyIdForNotification(companyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost("CreateNotificationTemplate")]
        public async Task<ActionResult> CreateNotificationTemplate([FromBody] NotificationTemplateRequestDTO request)
        {
            try
            {
                JsonResult? res = await _notificationSetupService.CreateNotificationTemplate(request);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("GetNotificationTemplateById/{notificationTemplateId}")]
        public async Task<ActionResult> GetNotificationTemplateById(int notificationTemplateId)
        {
            try
            {
                JsonResult? res = await _notificationSetupService.GetNotificationTemplateById(notificationTemplateId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("GetEmployeesByNotificationTemplateId/{notificationTemplateId}")]
        public async Task<ActionResult> GetEmployeesByNotificationTemplateId(int notificationTemplateId)
        {
            try
            {
                JsonResult? res = await _notificationSetupService.GetEmployeesByNotificationTemplateId(notificationTemplateId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("UpdateNotificationTemplate")]
        public async Task<ActionResult> UpdateNotificationTemplate([FromBody] NotificationTemplateRequestDTO request)
        {
            try
            {
                JsonResult? res = await _notificationSetupService.UpdateNotificationTemplate(request);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("DeleteNotificationTemplate/{notificationTemplateId}")]
        public async Task<ActionResult> DeleteNotificationTemplate(int notificationTemplateId)
        {
            try
            {
                JsonResult? res = await _notificationSetupService.DeleteNotificationTemplate(notificationTemplateId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }
}