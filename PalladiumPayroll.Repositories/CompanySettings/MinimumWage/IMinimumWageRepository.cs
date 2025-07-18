using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Repositories.CompanySettings;

public interface IMinimumWageRepository
{
     Task<bool> CreateMinimumWage(MinimumWageRequestDTO request);

     Task<bool> IsDuplicateMinimumWageName(string name, long companyId, int? wageId = null);

     Task<List<MinimumWageResponseDTO>> GetMinimumWagesByCompanyId(int companyId);

     Task<bool> UpdateMinimumWage(MinimumWageRequestDTO request);

     Task<bool> DeleteMinimumWage(int wageId);
}
