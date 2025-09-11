using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings.EmployeeProfile;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.CompanySettings.EmployeeProfile;

public class EmployeeProfileService : IEmployeeProfileService
{
    private readonly IEmployeeProfileRepository _employeeProfileRepository;

    public EmployeeProfileService(IEmployeeProfileRepository employeeProfileRepository)
    {
        _employeeProfileRepository = employeeProfileRepository;
    }
    public async Task<JsonResult> CreateProfile(EmployeeProfileRequestDTO request)
    {
        string message = await _employeeProfileRepository.CreateProfile(request);
        if (message == "Employee profile created successfully.")
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, message);
        }

        return HttpStatusCodeResponse.InternalServerErrorResponse(message);
    }
}
