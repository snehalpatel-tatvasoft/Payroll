using System.Net;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Company_Settings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;


namespace PalladiumPayroll.Controllers.Company_Settings
{

    [ApiController]
    [Route("api/[controller]")]
    public class DesignationsController : ControllerBase
    {
        private readonly IDesignationsService _designationsService;
        public DesignationsController(IDesignationsService designationsService)
        {
            _designationsService = designationsService;
        }

        [HttpPost("CreateDesignations")]
        public async Task<ActionResult> CreateDesignations([FromBody] DesignationRequestDTO request)
        {
            try
            {
                JsonResult? res = await _designationsService.CreateDesignations(request);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.Designations));
            }
        }

        [HttpGet("GetAllDesignations")]
        public async Task<ActionResult> GetAllDesignations([FromQuery] long companyId)
        {
            try
            {
                JsonResult? res = await _designationsService.GetAllDesignations(companyId);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.Designations));
            }
        }

        [HttpDelete("DeleteDesignations/{id}")]
        public async Task<ActionResult> DeleteDesignations(long id, long? employeeId)
        {
            try
            {
                JsonResult res = await _designationsService.DeleteDesignations(id, employeeId);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.Designations));
            }
        }

        [HttpPatch("UpdateDesignations")]
        public async Task<ActionResult> UpdateDesignations([FromBody] DesignationRequestDTO request)
        {
            try
            {
                JsonResult? res = await _designationsService.UpdateDesignations(request);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.Designations));
            }
        }
        
        [HttpPost("ImportDesignations")]
        public async Task<ActionResult> ImportDesignations([FromBody] ImportDesignationRequestDTO request)
        {
            try
            {
                JsonResult? res = await _designationsService.ImportDesignations(request);
                return res;
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Importing, ResponseMessages.Designations));
            }
        }
    }
}

