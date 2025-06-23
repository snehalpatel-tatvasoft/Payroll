using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Repositories.Company
{
    public interface ICompanyRepository
    {
        Task<long> CreateCompany(CreateCompanyRequest request);
        Task<bool> CreateUser(CreateUserRequestDto request);
        Task<bool> CheckCompanyExist(string company);
    }
}
