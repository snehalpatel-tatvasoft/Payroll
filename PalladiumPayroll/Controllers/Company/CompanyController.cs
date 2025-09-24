using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Company;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Company;
using System.Globalization;
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
        public async Task<ActionResult> GetCompanyWithSubCompany(int companyId, string userId)
        {
            try
            {
                List<DropDownViewModel> companyWithSubCompanyList = await _companyService.GetCompanyWithSubCompany(companyId, userId);
                return HttpStatusCodeResponse.SuccessResponse(companyWithSubCompanyList, string.Empty);
            }
            catch (Exception ex)
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

        [HttpGet("[action]")]
        public async Task<ActionResult> GetTransactionList(long companyId)
        {
            try
            {
                List<TransactionListForCompany> transactionLists = await _companyService.GetTransactionList(companyId);
                return HttpStatusCodeResponse.SuccessResponse(transactionLists, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> ExportGLTransactionList(long companyId)
        {
            try
            {
                var fileBytes = await _companyService.ExportGLTransactionList(companyId);
                return File(fileBytes, ContentTypes.Xlsx, "Transaction Information.xlsx");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ImportGLTransaction([FromForm] ImportFileModel requestData)
        {
            try
            {
                if (requestData.File.Length <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmptyFile);
                }
                else if (requestData.File.ContentType != ContentTypes.Xlsx)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InavalidFile);
                }
                return await _companyService.ImportGLTransaction(requestData);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SaveGlAccountNumber(TransactionListForCompany model)
        {
            try
            {
                bool res = await _companyService.SaveGlAccountNumber(model);
                return HttpStatusCodeResponse.SuccessResponse(res, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetCompanyGLInfo(int companyId)
        {
            try
            {
                List<GLSetup> glSetupInfo = await _companyService.GetCompanyGLInfo(companyId);
                return HttpStatusCodeResponse.SuccessResponse(glSetupInfo, string.Empty);
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
        public async Task<ActionResult> GetGLSetup([FromQuery] DBConnectionModel dbConnectionModel)
        {
            try
            {
                return await _companyService.GetGLSetup(dbConnectionModel);
            }
            catch (Exception ex)
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

        [HttpGet("[action]")]
        public async Task<ActionResult> GetProcessCyclePeriodInfo(int payrollId)
        {
            try
            {
                return await _companyService.GetProcessCyclePeriodInfo(payrollId);
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

        [HttpGet("[action]")]
        public async Task<ActionResult> GetIndustrialCouncil(int companyId)
        {
            try
            {
                IndustryCouncil councilInfo = await _companyService.GetIndustrialCouncil(companyId);
                return HttpStatusCodeResponse.SuccessResponse(councilInfo, string.Empty);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SaveIndustrialCouncil(IndustryCouncil industryCouncil)
        {
            try
            {
                return await _companyService.SaveIndustrialCouncil(industryCouncil);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult> GetMIBFADropdown(int companyId)
        {
            try
            {
                return await _companyService.GetMIBFADropdown(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

    }
}
