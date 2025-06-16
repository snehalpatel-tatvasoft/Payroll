using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
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
        public async Task<bool> CreateCompany(CreateCompanyRequest request)
        {
            bool response = await _companyRepository.CreateCompany(request);
            if (response)
            {
                return true;
            }
            return false;
        }
    }
}
