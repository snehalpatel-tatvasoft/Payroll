using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;

namespace PalladiumPayroll.Services.Company_Settings;

public interface IDesignationsService
{
    Task<JsonResult> CreateDesignations(DesignationRequestDto request);
    Task<JsonResult> GetAllDesignations(long companyId);
    Task<JsonResult> DeleteDesignations(long id);
    Task<JsonResult> UpdateDesignations(DesignationRequestDto request);
    Task<JsonResult> ImportDesignations(ImportDesignationRequestDto request);

}
