using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Company;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("CreateCompany")]
        [AllowAnonymous]
        public async Task<JsonResult> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            try
            {
                await _companyService.CreateCompany(request);
                //var response = await _authService.SignUp(model);
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Company, ActionType.Saving));
                //return HttpStatusCodeResponse.InternalServerErrorResponse("jfhdskjfhs");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }
    }
}
