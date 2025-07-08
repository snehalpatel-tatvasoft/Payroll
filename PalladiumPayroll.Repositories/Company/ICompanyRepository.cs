using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using System.ComponentModel.Design;

namespace PalladiumPayroll.Repositories.Company
{
    public interface ICompanyRepository
    {
        Task<long> CreateCompany(CreateCompanyRequest request);
        Task<Guid> CreateUser(CreateUserRequestDto request);
        Task<bool> CheckCompanyExist(string company);
        Task<bool> CheckCompanyExist(int companyId, string companyName);
        Task<JsonResult> CompanyCreation(CompanyModels model);
        Task<bool> AddNewBank(BankModel bankModel);
        Task<List<DropDownViewModel>> GetCompanyWithSubCompany(int companyId);
        Task<bool> SetActiveCompanyId(int companyId);
    }
}
