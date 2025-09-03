using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.TimeSheet;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.PayrollProcess.TimeSheet;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Controllers.PayrollProcess
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly ITimeSheetService _timeSheetService;
        public TimeSheetController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetLatestImportedData(int companyId)
        {
            try
            {
                return await _timeSheetService.GetLatestImportedData(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ImportTimeSheet([FromForm] ImportTimeSheetRequest requestData)
        {
            try
            {
                if(requestData.File.Length <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmptyFile);
                }
                else if(requestData.File.ContentType != ContentTypes.Xlsx)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InavalidFile);
                }
                return await _timeSheetService.ImportTimeSheet(requestData);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> ProcessTimeSheet()
        {
            try
            {
                return await _timeSheetService.ProcessTimeSheet();
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }
    }
}
