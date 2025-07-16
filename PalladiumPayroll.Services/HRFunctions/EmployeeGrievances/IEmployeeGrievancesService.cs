using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeGrievances;

namespace PalladiumPayroll.Services.HRFunctions.EmployeeGrievances;

public interface IEmployeeGrievancesService
{
    Task<JsonResult> UpsertEmployeeGrievance(EmployeeGrievanceUpsertData request);

    Task<JsonResult> DeleteEmployeeGrievance(long employeeGrievanceId, string userId);

    Task<JsonResult> GetEmployeesForGrievances(long  companyId);

    Task<JsonResult> GetNatureOfGrievances(long  companyId);

    Task<JsonResult> GetEmployeeGrievances(long companyId);

    Task<JsonResult> GetEmployeeGrievanceById(long grievanceId);
}
