using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Repositories.Company
{
    public interface ICompanyRepository
    {
        Task<bool> CreateCompany(CreateCompanyRequest request);
    }
}
