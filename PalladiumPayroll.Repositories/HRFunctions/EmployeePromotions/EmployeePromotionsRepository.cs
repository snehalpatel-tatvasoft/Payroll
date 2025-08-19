using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeePromotions;

namespace PalladiumPayroll.Repositories.HRFunctions.EmployeePromotions;

public class EmployeePromotionsRepository : IEmployeePromotionsRepository
{
    private readonly DapperContext _dapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployeePromotionsRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _dapper = new DapperContext(configuration);
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> AddEmployeePromotion(EmployeePromotionsUpsertData request)
    {
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@ReportToId", request.ReportToId);
        parameters.Add("@EmployeeInitialsSurname", request.EmployeeInitialsSurname);
        parameters.Add("@DesignationId", request.DesignationId);
        parameters.Add("@JobGradeId", request.JobGradeId);
        parameters.Add("@WSPCategoryId", request.WSPCategoryId);
        parameters.Add("@OFOCodeId", request.OFOCodeId);
        parameters.Add("@MajorCostCentreId", request.MajorCostCentreId);
        parameters.Add("@NICGradeId", request.NICGradeId);
        parameters.Add("@OccupationalCategoryId", request.OccupationalCategoryId);
        parameters.Add("@OccupationalLevelId", request.OccupationalLevelId);
        parameters.Add("@EffectiveDate", request.EffectiveDate);
        parameters.Add("@BranchId", request.BranchId);
        parameters.Add("@DepartmentId", request.DepartmentId);
        parameters.Add("@ProvinceId", request.ProvinceId);
        parameters.Add("@SupportFunctionId", request.SupportFunctionId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_AddEmployeePromotion", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }

    public async Task<bool> UpdateEmployeePromotion(EmployeePromotionsUpsertData request)
    {
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@EmployeePromotionsId", request.EmployeePromotionsId);
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@ReportToId", request.ReportToId);
        parameters.Add("@EmployeeInitialsSurname", request.EmployeeInitialsSurname);
        parameters.Add("@DesignationId", request.DesignationId);
        parameters.Add("@JobGradeId", request.JobGradeId);
        parameters.Add("@WSPCategoryId", request.WSPCategoryId);
        parameters.Add("@OFOCodeId", request.OFOCodeId);
        parameters.Add("@MajorCostCentreId", request.MajorCostCentreId);
        parameters.Add("@NICGradeId", request.NICGradeId);
        parameters.Add("@OccupationalCategoryId", request.OccupationalCategoryId);
        parameters.Add("@OccupationalLevelId", request.OccupationalLevelId);
        parameters.Add("@EffectiveDate", request.EffectiveDate);
        parameters.Add("@BranchId", request.BranchId);
        parameters.Add("@DepartmentId", request.DepartmentId);
        parameters.Add("@ProvinceId", request.ProvinceId);
        parameters.Add("@SupportFunctionId", request.SupportFunctionId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpdateEmployeePromotion", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }

    public async Task<bool> DeleteEmployeePromotion(long employeePromotionId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeePromotionId", employeePromotionId);
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
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
                    Designations = (await multi.ReadAsync<DesignationDto>()).ToList(),
                    ReportTos = (await multi.ReadAsync<ReportToEmployeeDto>()).ToList(),
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
            "usp_GetEmployeePromotions",
            parameters
        );
        return result;
    }

    public async Task<EmployeePromotionDetailDTO?> GetEmployeePromotioneById(long employeePromotionId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeePromotionId", employeePromotionId);

        return await _dapper.ExecuteStoredProcedureSingle<EmployeePromotionDetailDTO>(
            "usp_GetEmployeePromotionById", parameters);
    }


    public async Task<EmployeePromotionAutoFillDTO?> GetEmployeePromotionAutofillData(long employeeId, long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@employee_id", employeeId);
        parameters.Add("@company_id", companyId);

        return await _dapper.ExecuteStoredProcedureSingle<EmployeePromotionAutoFillDTO>(
            "usp_GetEmployeeTransferAutofilldata", parameters);

    }


}
