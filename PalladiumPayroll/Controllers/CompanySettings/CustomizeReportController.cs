using Microsoft.AspNetCore.Mvc;
using System.Net;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class CustomizeReportController : ControllerBase
{
    private readonly ICustomizeReportService _customizeReportService;
    private readonly string _baseReportPath;

    public CustomizeReportController(ICustomizeReportService customizeReportService, IConfiguration configuration)
    {
        _customizeReportService = customizeReportService;
        _baseReportPath = configuration.GetValue<string>("ReportSettings:BaseReportPath")
                         ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports");
    }

    [HttpGet("GetAllReports")]
    public async Task<ActionResult> GetAllReports()
    {
        try
        {
            JsonResult? res = await _customizeReportService.GetAllReports(new CustomizeReportRequestDTO());
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

    [HttpPost("DownloadReport")]
    public async Task<ActionResult> DownloadReport([FromBody] DownloadReportRequestDTO request)
    {
        try
        {
            JsonResult? res = await _customizeReportService.DownloadReport(request);
            if (res.Value is HttpApiResponse<DownloadReportResponseDTO> response && response.Result)
            {
                var relativePath = response.Data?.ReportPath;
                if (string.IsNullOrEmpty(relativePath))
                {
                    return NotFound(new HttpApiResponse<object>
                    {
                        Result = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Report path not found.",
                        Data = null
                    });
                }

                var absolutePath = Path.Combine(_baseReportPath, relativePath.Replace('/', Path.DirectorySeparatorChar));
                if (!System.IO.File.Exists(absolutePath))
                {
                    return NotFound(new HttpApiResponse<object>
                    {
                        Result = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Report file not found at {absolutePath}.",
                        Data = null
                    });
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(absolutePath);
                var fileName = Path.GetFileName(absolutePath);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            return Ok(res.Value);
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
    
    [HttpPost("UploadReport")]
    public async Task<ActionResult> UploadReport([FromForm] UploadReportRequestDTO request)
    {
        try
        {
            if (!Directory.Exists(_baseReportPath))
            {
                Directory.CreateDirectory(_baseReportPath);
            }

            var uploadDir = Path.Combine(_baseReportPath, "uploaded-reports");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileNameWithoutExtension(request.File.FileName)}.repx";
            var filePath = Path.Combine(uploadDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var result = await _customizeReportService.UploadReport(request, fileName);
            return result;
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
            {
                Result = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"Failed to upload report: {ex.Message}",
                Data = null
            });
        }
    }
}