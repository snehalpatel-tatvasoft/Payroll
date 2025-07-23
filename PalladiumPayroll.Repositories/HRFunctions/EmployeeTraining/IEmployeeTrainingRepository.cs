using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;

namespace PalladiumPayroll.Repositories.HRFunctions.EmployeeTraining;

public interface IEmployeeTrainingRepository
{
    Task<bool> UpsertEmployeeTraining(EmployeeTrainingUpsertData request);

    Task<bool> DeleteEmployeeTraining(long employeeTrainingId, string userId);

    Task<List<EmployeeTrainingDisplayDataDTO>> GetEmployeeTrainingDisplayData(long companyId);

     Task<EmployeeTrainingDetailDTO?> GetEmployeeTrainingById(long employeeTrainingId);

     Task<EmployeeTrainingDropdownsDTO> GetEmployeeTrainingDropdownData(long companyId);
}
