using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Company;

namespace PalladiumPayroll.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<JsonResult> CompanyCreation(CompanyModels model)
        {
            return await _companyRepository.CompanyCreation(model);
        }

        public async Task<bool> AddNewBank(BankModel bankModel)
        {
            return await _companyRepository.AddNewBank(bankModel);
        }
    }
}
