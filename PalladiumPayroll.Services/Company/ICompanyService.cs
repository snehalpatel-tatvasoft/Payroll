using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Services.Company
{
    public interface ICompanyService
    {
        Task<bool> CreateCompany(CreateCompanyRequest request);
    }
}
