using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;

namespace PalladiumPayroll.Services.Company
{
    public interface ICompanyService
    {
        Task<JsonResult> CheckCompanyExist(int companyId, string companyName);
        Task<JsonResult> CreateCompany(CreateCompanyRequest request);
        Task<JsonResult> CompanyCreation(CompanyModels model);
        Task<JsonResult> AddNewBank(BankModel bankModel);
        Task<List<DropDownViewModel>> GetCompanyWithSubCompany(int companyId);
        Task<JsonResult> SetActiveCompanyId(int companyId);
        Task<List<CompanyInfo>> GetCompanyInformation(int companyId);
        Task<JsonResult> UpdateCompanyInformation(CompanyInfo companyInfo);
    }
}
