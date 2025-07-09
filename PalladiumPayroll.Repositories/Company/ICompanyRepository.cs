using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;

namespace PalladiumPayroll.Repositories.Company
{
    public interface ICompanyRepository
    {
        Task<long> CreateCompany(CreateCompanyRequest request);
        Task<Guid> CreateUser(CreateUserRequestDto request);
        Task<bool> CheckCompanyExist(string company);
        Task<bool> CheckCompanyExist(CheckCompanyExistModel reqModel);
        Task<JsonResult> CompanyCreation(CompanyModels model);
        Task<bool> AddNewBank(BankModel bankModel);
        Task<List<DropDownViewModel>> GetCompanyWithSubCompany(int companyId);
        Task<bool> SetActiveCompanyId(int companyId);
        Task<List<CompanyInfo>> GetCompanyInformation(int companyId);
        Task<bool> UpdateCompanyInformation(CompanyInfo companyInfo);
        Task<List<CompanyRepresentative>> GetCompanyRepresentativeInfo(int companyId);
        Task<bool> UpdateCompanyRepresentativeInfo(CompanyRepresentative companyRepresentativeInfo);
        Task<List<CompanyBankAccount>> GetBankDetailsInfo(int companyId);
        Task<bool> UpdateBankDetailsInfo(CompanyBankAccount companyBankAccount);
        Task<List<CompanyPayrollCycle>> GetPayrollCycleInfo(int companyId, int taxYearId);
        Task<bool> UpsertPayrollCycleInfo(CompanyPayrollCycle companyPayrollCycle);
        Task<bool> DeletePayrollCycleInfo(int cycleId);
    }
}
