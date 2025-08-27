using PalladiumPayroll.DTOs.DTOs.HRFunctions.CoidAccident;

namespace PalladiumPayroll.Repositories.HRFunctions.CoidAccident;

public interface ICoidAccidentRepository
{
    Task<bool> UpsertCOIDAccident(CoidAccidentRequestDTO request);
    Task<List<CoidAccidentResponseDTO>> GetAccidentsByCompanyId(long companyId);
    Task<bool> DeleteCoidAccident(long coidAccidentId);

}
