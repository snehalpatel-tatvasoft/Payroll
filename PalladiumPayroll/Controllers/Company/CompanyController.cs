using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
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
        public async Task<ActionResult> CheckCompanyExist([FromQuery] CheckCompanyExistModel reqModel)
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

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBankDetailsInfo(int companyId)
        {
            try
            {
                List<CompanyBankAccount> companyInfo = await _companyService.GetBankDetailsInfo(companyId);
                return HttpStatusCodeResponse.SuccessResponse(companyInfo, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateBankDetailsInfo(CompanyBankAccount companyBankAccount)
        {
            try
            {
                return await _companyService.UpdateBankDetailsInfo(companyBankAccount);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetPayrollCycleInfo(int companyId, int taxYearId)
        {
            try
            {
                List<CompanyPayrollCycle> companyInfo = await _companyService.GetPayrollCycleInfo(companyId, taxYearId);
                return HttpStatusCodeResponse.SuccessResponse(companyInfo, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetMedicalAidFundInfo(int companyId)
        {
            try
            {
                List<PayrollMedicalAidList> companyInfo = await _companyService.GetMedicalAidFundInfo(companyId);
                return HttpStatusCodeResponse.SuccessResponse(companyInfo, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetCompanyBenefitFundInfo(int companyId)
        {
            try
            {
                List<PayrollBenefitFundList> companyInfo = await _companyService.GetCompanyBenefitFundInfo(companyId);
                return HttpStatusCodeResponse.SuccessResponse(companyInfo, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertPayrollCycleInfo(CompanyPayrollCycle companyPayrollCycle)
        {
            try
            {
                return await _companyService.UpsertPayrollCycleInfo(companyPayrollCycle);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertCompanyBenefitFund(PayrollBenefitFundList payrollBenefitFundList)
        {
            try
            {
                return await _companyService.UpsertCompanyBenefitFund(payrollBenefitFundList);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertCOIDASetupInfo(CompanyCoidaSetup companyCoidaSetup)
        {
            try
            {
                return await _companyService.UpsertCOIDASetupInfo(companyCoidaSetup);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeletePayrollCycleInfo(int cycleId)
        {
            try
            {
                return await _companyService.DeletePayrollCycleInfo(cycleId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteMedicalAidFund(int fundId)
        {
            try
            {
                return await _companyService.DeleteMedicalAidFund(fundId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteCompanyBenefitFund(int fundId)
        {
            try
            {
                return await _companyService.DeleteCompanyBenefitFund(fundId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> AddMedicalAidFundInfo(PayrollMedicalAidList payrollMedicalAidList)
        {
            try
            {
                return await _companyService.AddMedicalAidFundInfo(payrollMedicalAidList);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetCOIDASetupInfo(int companyId, int yearId)
        {
            try
            {
                List<CompanyCoidaSetup> companyInfo = await _companyService.GetCOIDASetupInfo(companyId, yearId);
                return HttpStatusCodeResponse.SuccessResponse(companyInfo, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmploymentEquityInfo(int companyId)
        {
            try
            {
                List<EmploymentEquityInformation> companyInfo = await _companyService.GetEmploymentEquityInfo(companyId);
                return HttpStatusCodeResponse.SuccessResponse(companyInfo, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertEmploymentEquityInfo(EmploymentEquityInformation employmentEquityInformation)
        {
            try
            {
                return await _companyService.UpsertEmploymentEquityInfo(employmentEquityInformation);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
