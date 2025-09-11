using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

namespace PalladiumPayroll.Repositories.CompanySettings.EmployeeProfile;

public interface IEmployeeProfileRepository
{
    Task<string> CreateProfile(EmployeeProfileRequestDTO request);

}
