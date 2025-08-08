using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeCodes;

namespace PalladiumPayroll.Repositories.CompanySettings;

public interface IEmployeeCodesRepository
{
    Task<bool> SaveEmployeeCodeGeneration(EmployeeCodeRequestDTO request);

    Task<EmployeeCodeResponseDTO?> GetEmployeeCodeByCompanyId(int companyId);
}
