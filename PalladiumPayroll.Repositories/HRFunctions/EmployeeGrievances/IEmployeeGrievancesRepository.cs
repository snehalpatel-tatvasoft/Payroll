using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeGrievances;

namespace PalladiumPayroll.Repositories.HRFunctions.EmployeeGrievances;

public interface IEmployeeGrievancesRepository
{
    Task<bool> UpsertEmployeeGrievance(EmployeeGrievanceUpsertData request);

    Task<bool> DeleteEmployeeGrievance(long employeeGrievanceId, string userId);

    Task<List<EmployeeDropdownDTO>> GetEmployeesForGrievances(long companyId);

    Task<List<NatureOfGrievanceDTO>> GetNatureOfGrievances(long companyId);

    Task<List<EmployeeGrievanceDTO>> GetEmployeeGrievances(long companyId);

    Task<EmployeeGrievanceDTO?> GetEmployeeGrievanceById(long grievanceId);

    Task<List<DropDownViewModel>> AddNatureOfGrievance(NatureOfGrievancesDto request);

    Task<bool> DeleteNatureOfGrievance(int natureOfGrievanceId);
}
