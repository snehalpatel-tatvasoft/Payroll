using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;

namespace PalladiumPayroll.Services.HRFunctions.EmployeeTraining;

public interface IEmployeeTrainingService
{
    Task<JsonResult> UpsertEmployeeTraining(EmployeeTrainingUpsertData request);

    Task<JsonResult> DeleteEmployeeTraining(long employeeTrainingId, string userId);

    Task<JsonResult> GetEmployeeTrainingDropdownData(long companyId);

    Task<JsonResult> GetEmployeeTrainings(long companyId);

    Task<JsonResult> GetEmployeeTrainingById(long trainingId);

     Task<string?> UploadTrainingFile(IFormFile file);
}
