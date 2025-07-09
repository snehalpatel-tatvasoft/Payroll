using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;

namespace PalladiumPayroll.Services.Company
{
    public interface ICompanyService
    {
        Task<JsonResult> CheckCompanyExist(CheckCompanyExistModel reqModel);
        Task<JsonResult> CreateCompany(CreateCompanyRequest request);
        Task<JsonResult> CompanyCreation(CompanyModels model);
        Task<JsonResult> AddNewBank(BankModel bankModel);
        Task<List<DropDownViewModel>> GetCompanyWithSubCompany(int companyId);
        Task<JsonResult> SetActiveCompanyId(int companyId);
        Task<List<CompanyInfo>> GetCompanyInformation(int companyId);
        Task<JsonResult> UpdateCompanyInformation(CompanyInfo companyInfo);
        Task<List<CompanyRepresentative>> GetCompanyRepresentativeInfo(int companyId);
        Task<JsonResult> UpdateCompanyRepresentativeInfo(CompanyRepresentative companyRepresentativeInfo);
        Task<List<CompanyBankAccount>> GetBankDetailsInfo(int companyId);
        Task<JsonResult> UpdateBankDetailsInfo(CompanyBankAccount companyBankAccount);
        Task<List<CompanyPayrollCycle>> GetPayrollCycleInfo(int companyId, int taxYearId);
        Task<JsonResult> UpsertPayrollCycleInfo(CompanyPayrollCycle companyPayrollCycle);
        Task<JsonResult> DeletePayrollCycleInfo(int cycleId);
    }
}
