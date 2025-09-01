using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.CoidAccident;

namespace PalladiumPayroll.Services.HRFunctions.CoidAccident;

public interface ICoidAccidentService
{
    Task<JsonResult> UpsertCOIDAccident(CoidAccidentRequestDTO request);
    Task<JsonResult> GetAccidentsByCompanyId(long companyId);
    Task<JsonResult> DeleteCoidAccident(long coidAccidentId);

}
