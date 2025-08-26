using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.HRFunctions.DisciplinaryLog;
using System.Net;
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

        // [HttpPost("CreateDisciplinaryLog")]
        // public async Task<ActionResult> CreateDisciplinaryLog([FromForm] DisciplinaryLogRequestDTO disciplinaryLog, IFormFile file)
        // {
        //     try
        //     {
        //         JsonResult? res = await _disciplinaryLogService.CreateDisciplinaryLog(disciplinaryLog, file);
        //         return res;
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
        //         {
        //             Result = false,
        //             StatusCode = HttpStatusCode.InternalServerError,
        //             Message = ex.Message,
        //             Data = null
        //         });
        //     }
        // }

        [HttpGet("GetDisciplinaryLogById/{disciplinaryLogId}")]
        public async Task<ActionResult> GetDisciplinaryLogById(long disciplinaryLogId)
        {
            try
            {
                JsonResult? res = await _disciplinaryLogService.GetDisciplinaryLogById(disciplinaryLogId);
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

        // [HttpPut("UpdateDisciplinaryLog")]
        // public async Task<ActionResult> UpdateDisciplinaryLog([FromForm] DisciplinaryLogEditRequestDTO disciplinaryLog, IFormFile file)
        // {
        //     try
        //     {
        //         JsonResult? res = await _disciplinaryLogService.UpdateDisciplinaryLog(disciplinaryLog, file);
        //         return res;
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
        //         {
        //             Result = false,
        //             StatusCode = HttpStatusCode.InternalServerError,
        //             Message = ex.Message,
        //             Data = null
        //         });
        //     }
        // }

        [HttpDelete("DeleteDisciplinaryLog/{disciplinaryLogId}")]
        public async Task<ActionResult> DeleteDisciplinaryLog(long disciplinaryLogId)
        {
            try
            {
                JsonResult? res = await _disciplinaryLogService.DeleteDisciplinaryLog(disciplinaryLogId);
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

        [HttpGet("DownloadFile/{disciplinaryLogId}")]
        public async Task<IActionResult> DownloadFile(long disciplinaryLogId)
        {
            try
            {
                var logResult = await _disciplinaryLogService.GetDisciplinaryLogById(disciplinaryLogId);
                if (logResult == null)
                {
                    return NotFound(new HttpApiResponse<object>
                    {
                        Result = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "File not found",
                        Data = null
                    });
                }

                // Assuming HttpApiResponse<T> is the structure used by HttpStatusCodeResponse
                var responseData = logResult.Value as HttpApiResponse<DisciplinaryLogByIdResponseDTO>;
                if (responseData == null || responseData.Result == false || responseData.Data == null || string.IsNullOrEmpty(responseData.Data.FilePath))
                {
                    return NotFound(new HttpApiResponse<object>
                    {
                        Result = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "File not found",
                        Data = null
                    });
                }

                var filePath = responseData.Data.FilePath;
                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/octet-stream", responseData.Data.FileName);
                }
                return NotFound(new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "File not found on server",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Error downloading file: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}