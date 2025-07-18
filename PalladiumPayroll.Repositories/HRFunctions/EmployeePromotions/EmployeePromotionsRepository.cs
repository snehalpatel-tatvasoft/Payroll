using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeePromotions;

namespace PalladiumPayroll.Repositories.HRFunctions.EmployeePromotions;

public class EmployeePromotionsRepository : IEmployeePromotionsRepository
{
    private readonly DapperContext _dapper;

    public EmployeePromotionsRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<bool> UpsertEmployeePromotions(EmployeePromotionsUpsertData request)
    {
        DynamicParameters? parameters = new DynamicParameters();
        
        // all parameters
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertEmployeePromotions", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }


    public async Task<bool> DeleteEmployeePromotion(long employeePromotionId, string userId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeePromotionId", employeePromotionId);
        parameters.Add("@UserId", userId);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_DeleteEmployeePromotion", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }


    public async Task<EmployeePromotionDropdownsDTO> GetEmployeePromotionDropdownData(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        return await _dapper.ExecuteStoredProcedureMultipleAsync(
            "usp_GetDropdownDataForEmployeePromotion",
            parameters,
            async multi =>
            {
                EmployeePromotionDropdownsDTO? dropdownsData = new EmployeePromotionDropdownsDTO
                {
                    JobGrades = (await multi.ReadAsync<JobGradeDto>()).ToList(),
                    WSPCategories = (await multi.ReadAsync<WSPCategoryDto>()).ToList(),
                    OFOCodes = (await multi.ReadAsync<OFOCodeDto>()).ToList(),
                    MajorCostCenters = (await multi.ReadAsync<MajorCostCenterDto>()).ToList(),
                    NICGrades = (await multi.ReadAsync<NICGradeDto>()).ToList(),
                    OccupationalCategories = (await multi.ReadAsync<OccupationalCategoryDto>()).ToList(),
                    OccupationalLevels = (await multi.ReadAsync<OccupationalLevelDto>()).ToList(),
                    Branches = (await multi.ReadAsync<BranchDto>()).ToList(),
                    Provinces = (await multi.ReadAsync<ProvinceDto>()).ToList(),
                    SupportFunctions = (await multi.ReadAsync<SupportFunctionDto>()).ToList(),
                    Departments = (await multi.ReadAsync<DepartmentDto>()).ToList(),
                    Employees = (await multi.ReadAsync<EmployeeDropdownDto>()).ToList(),
                    DesignationCodes = (await multi.ReadAsync<DesignationCodeDto>()).ToList()
                };
                return dropdownsData;
            }
        );
    }


    public async Task<List<EmployeePromotionsdisplayDataDTO>> GetEmployeePromotionsDisplayData(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<EmployeePromotionsdisplayDataDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeePromotionsdisplayDataDTO>(
            "usp_GetEmployeePromotionDisplayData",
            parameters
        );
        return result;
    }

    public async Task<EmployeePromotionDetailDTO?> GetEmployeePromotioneById(long promotionId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@PromotionId", promotionId);

        return await _dapper.ExecuteStoredProcedureSingle<EmployeePromotionDetailDTO>(
            "usp_GetEmployeePromotionById", parameters);
    }

}
