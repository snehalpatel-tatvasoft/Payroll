using System.Net;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;
using PalladiumPayroll.Services.Company_Settings;

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
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpDelete("DeleteDesignations/{id}")]
        public async Task<ActionResult> DeleteDesignations(long id)
        {
            try
            {
                JsonResult res = await _designationsService.DeleteDesignations(id);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}

