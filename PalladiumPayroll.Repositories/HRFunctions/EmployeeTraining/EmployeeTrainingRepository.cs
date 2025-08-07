using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;

namespace PalladiumPayroll.Repositories.HRFunctions.EmployeeTraining;

public class EmployeeTrainingRepository : IEmployeeTrainingRepository
{
    private readonly DapperContext _dapper;

    public EmployeeTrainingRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<bool> UpsertEmployeeTraining(EmployeeTrainingUpsertData request)
    {
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@EmployeeTrainingId", request.EmployeeTrainingId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@TrainingDate", request.TrainingDate);
        parameters.Add("@CourseId", request.CourseId);
        parameters.Add("@Duration", request.Duration);
        parameters.Add("@DurationId", request.DurationId);
        parameters.Add("@ActualCost", request.ActualCost);
        parameters.Add("@BudgetCost", request.BudgetCost);
        parameters.Add("@CourseTypeId", request.CourseTypeId);
        parameters.Add("@InstitutionId", request.InstitutionId);
        parameters.Add("@CertificateNumber", request.CertificateNumber);
        parameters.Add("@ResultId", request.ResultId);
        parameters.Add("@CourseStatusId", request.CourseStatusId);
        parameters.Add("@TrainerDetails", request.TrainerDetails);
        parameters.Add("@NQFLevelId", request.NQFLevelId);
        parameters.Add("@UnitStandardId", request.UnitStandardId);
        parameters.Add("@SAQARequired", request.SAQARequired);
        parameters.Add("@Comments", request.Comments);
        parameters.Add("@FilePath", request.FilePath);
        parameters.Add("@UserId", request.UserId);

        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertEmployeeTraining", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }


    public async Task<bool> DeleteEmployeeTraining(long employeeTrainingId, string userId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeTrainingId", employeeTrainingId);
        parameters.Add("@UserId", userId);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_DeleteEmployeeTraining", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }


    public async Task<List<EmployeeTrainingDisplayDataDTO>> GetEmployeeTrainingDisplayData(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<EmployeeTrainingDisplayDataDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeeTrainingDisplayDataDTO>(
            "usp_GetEmployeeTrainings",
            parameters
        );
        return result;
    }


    public async Task<EmployeeTrainingDetailDTO?> GetEmployeeTrainingById(long employeeTrainingId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeTrainingId", employeeTrainingId);

        return await _dapper.ExecuteStoredProcedureSingle<EmployeeTrainingDetailDTO>(
            "usp_GetEmployeeTrainingById", parameters);
    }


    public async Task<EmployeeTrainingDropdownsDTO> GetEmployeeTrainingDropdownData(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        return await _dapper.ExecuteStoredProcedureMultipleAsync(
            "usp_GetDropdownDataForEmployeeTraining",
            parameters,
            async multi =>
            {
                EmployeeTrainingDropdownsDTO? dropdownsData = new EmployeeTrainingDropdownsDTO
                {
                    Courses = (await multi.ReadAsync<CourseDto>()).ToList(),
                    CourseTypes = (await multi.ReadAsync<CourseTypeDto>()).ToList(),
                    Institutions = (await multi.ReadAsync<InstitutionDto>()).ToList(),
                    NQFLevels = (await multi.ReadAsync<NQFLevelDto>()).ToList(),
                    UnitStandards = (await multi.ReadAsync<UnitStandardDto>()).ToList(),
                    Durations = (await multi.ReadAsync<DurationDto>()).ToList(),
                    Results = (await multi.ReadAsync<ResultDto>()).ToList(),
                    CourseStatuses = (await multi.ReadAsync<CourseStatusDto>()).ToList(),
                    Employees = (await multi.ReadAsync<EmployeeDto>()).ToList(),
                };
                return dropdownsData;
            }
        );
    }

    public async Task<List<DropDownViewModel>> AddEmployeeTrainingDropdownItem(EmployeeTrainingDropdownItem reqItem)
    {
        List<DropDownViewModel>? result = new List<DropDownViewModel>();
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@Name", reqItem.Name);
        parameters.Add("@CompanyId", reqItem.CompanyId);

        switch (reqItem.Type)
        {
            case 1:
                result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddCourse", parameters);
                break;
            case 2:
                result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddCourseType", parameters);
                break;
            case 3:
                result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddInstitution", parameters);
                break;
            case 4:
                result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddNQFLevel", parameters);
                break;
            case 5:
                result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddUnitStandard", parameters);
                break;
        }
        return result;
    }

    public async Task<bool> DeleteEmployeeTrainingDropdownItem(int id, int type)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@id", id);
        parameters.Add("@type", type);
        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteEmployeeTrainingDropdownItem", parameters);
    }

}
