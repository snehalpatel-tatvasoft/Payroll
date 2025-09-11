using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

namespace PalladiumPayroll.Services.CompanySettings.EmployeeProfile;

public interface IEmployeeProfileService
{
    
    Task<JsonResult> CreateProfile(EmployeeProfileRequestDTO request);

}
