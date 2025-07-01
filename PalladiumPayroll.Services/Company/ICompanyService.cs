using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Services.Company
{
    public interface ICompanyService
    {
        Task<JsonResult> CreateCompany(CreateCompanyRequest request);
        Task<JsonResult> CompanyCreation(CompanyModels model);
        Task<bool> AddNewBank(BankModel bankModel);

    }
}
