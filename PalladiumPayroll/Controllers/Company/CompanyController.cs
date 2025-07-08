using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Company;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Controllers.Company
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


        [HttpGet("[action]")]
        public async Task<ActionResult> CheckCompanyExist([FromQuery]CheckCompanyExistModel reqModel)
        {
            try
            {
                return await _companyService.CheckCompanyExist(reqModel);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CompanyCreation(CompanyModels model)
        {
            try
            {
                return await _companyService.CompanyCreation(model);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> AddBank(BankModel bankModel)
        {
            try
            {
                return await _companyService.AddNewBank(bankModel);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetCompanyWithSubCompany(int companyId)
        {
            try
            {
                List<DropDownViewModel> companyWithSubCompanyList = await _companyService.GetCompanyWithSubCompany(companyId);
                return HttpStatusCodeResponse.SuccessResponse(companyWithSubCompanyList, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> SetActiveCompanyId(int companyId)
        {
            try
            {
                return await _companyService.SetActiveCompanyId(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetCompanyInformation(int companyId)
        {
            try
            {
                List<CompanyInfo> companyInfo = await _companyService.GetCompanyInformation(companyId);
                return HttpStatusCodeResponse.SuccessResponse(companyInfo, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateCompanyInformation(CompanyInfo companyInfo)
        {
            try
            {
                return await _companyService.UpdateCompanyInformation(companyInfo);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetCompanyRepresentativeInfo(int companyId)
        {
            try
            {
                List<CompanyRepresentative> companyInfo = await _companyService.GetCompanyRepresentativeInfo(companyId);
                return HttpStatusCodeResponse.SuccessResponse(companyInfo, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateCompanyRepresentativeInfo(CompanyRepresentative companyRepresentative)
        {
            try
            {
                return await _companyService.UpdateCompanyRepresentativeInfo(companyRepresentative);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
