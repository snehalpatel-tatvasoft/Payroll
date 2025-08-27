using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.HRFunctions.DisciplinaryLog;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.DisciplinaryLog
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaryLogController : ControllerBase
    {
        private readonly IDisciplinaryLogService _disciplinaryLogService;

        public DisciplinaryLogController(IDisciplinaryLogService disciplinaryLogService)
        {
            _disciplinaryLogService = disciplinaryLogService;
        }

        [HttpGet("GetDisciplinaryLogByCompanyId/{companyId}")]
        public async Task<ActionResult> GetDisciplinaryLogByCompanyId(long companyId)
        {
            try
            {
                JsonResult? res = await _disciplinaryLogService.GetDisciplinaryLogByCompanyId(companyId);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.DisciplinaryLog)
                );
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertDisciplinaryLog([FromForm] DisciplinaryLogUpsertDTO request)
        {
            try
            {
                if (request.DisciplinaryLogId < 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidDisciplinaryLogId);
                }
                return await _disciplinaryLogService.UpsertDisciplinaryLog(request);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.DisciplinaryLog)
                );
            }
        }

        [HttpGet("GetEmployeesForDisciplinaryLogDropdown/{companyId}")]
        public async Task<ActionResult> GetEmployeesForDisciplinaryLogDropdown(long companyId)
        {
            try
            {
                JsonResult? res = await _disciplinaryLogService.GetEmployeesForDisciplinaryLogDropdown(companyId);
                return res;
            }
            catch (Exception)
            {
                 return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.DisciplinaryLog+" Droplist")
                );
            }
        }


        [HttpGet("GetDisciplinaryLogById/{disciplinaryLogId}")]
        public async Task<ActionResult> GetDisciplinaryLogById(long disciplinaryLogId)
        {
            try
            {
                JsonResult? res = await _disciplinaryLogService.GetDisciplinaryLogById(disciplinaryLogId);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.DisciplinaryLog)
                );
            }
        }


        [HttpDelete("DeleteDisciplinaryLog/{disciplinaryLogId}")]
        public async Task<ActionResult> DeleteDisciplinaryLog(long disciplinaryLogId)
        {
            try
            {
                JsonResult? res = await _disciplinaryLogService.DeleteDisciplinaryLog(disciplinaryLogId);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.DisciplinaryLog)
            );
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> DownloadDisciplinaryLogDocument(string fileUrl)
        {
            try
            {
                return File(await _disciplinaryLogService.DownloadDisciplinaryLogDocument(fileUrl), "application/octet-stream", fileUrl.Split("\\").LastOrDefault());
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, "downloading", ResponseMessages.DisciplinaryLog + " documnet"));
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteDisciplinaryLogDocument([FromQuery] DeleteDisplinaryLogDocumentDTO reqModel)
        {
            try
            {
                return await _disciplinaryLogService.DeleteDisciplinaryLogDocument(reqModel.DocumentUrl);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.DisciplinaryLog + " documnet."));
            }
        }
    }
}