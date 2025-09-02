using Microsoft.AspNetCore.Mvc;
using System.Net;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class CustomizeReportController : ControllerBase
{
    private readonly ICustomizeReportService _customizeReportService;

    public CustomizeReportController(ICustomizeReportService customizeReportService, IConfiguration configuration)
    {
        _customizeReportService = customizeReportService;
    }

    [HttpGet("GetAllReports")]
    public async Task<ActionResult> GetAllReports()
    {
        try
        {
            JsonResult? res = await _customizeReportService.GetAllReports(new CustomizeReportRequestDTO());
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CustomizeReport)
                );
        }
    }

    [HttpGet("DownloadReport")]
    public async Task<ActionResult> DownloadReport([FromQuery] int reportId, [FromQuery] int? companyId)
    {
        try
        {
            var request = new DownloadReportRequestDTO
            {
                ReportId = reportId,
                CompanyId = companyId
            };
            JsonResult? res = await _customizeReportService.DownloadReport(request);
            if (res.Value is HttpApiResponse<DownloadReportResponseDTO> response && response.Result)
            {
                var filePath = response.Data?.FilePath;
                if (string.IsNullOrEmpty(filePath))
                {
                    return HttpStatusCodeResponse.NotFoundResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CustomizeReport));                  

                }

                if (!System.IO.File.Exists(filePath))
                {
                    return HttpStatusCodeResponse.NotFoundResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CustomizeReport));                  
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                var fileName = Path.GetFileName(filePath);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            return Ok(res.Value);
        }
        catch (Exception )
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
            string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CustomizeReport));                  
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> UploadReport([FromForm] UploadReportRequestDTO request)
    {
        try
        {
            if (request.ReportId < 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidReportId);
            }
            return await _customizeReportService.UploadReport(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.CustomizeReport)
            );
        }
    }

}