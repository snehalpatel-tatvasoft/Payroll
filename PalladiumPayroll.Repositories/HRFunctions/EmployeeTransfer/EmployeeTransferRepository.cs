using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTransfer;
using PalladiumPayroll.DTOs.HRFunctions.EmployeeTransfer;


namespace PalladiumPayroll.Repositories.HRFunctions.EmployeeTransfer;

public class EmployeeTransferRepository : IEmployeeTransferRepository
{
    private readonly DapperContext _dapper;

    public EmployeeTransferRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<EmployeeTransferDropdownsDTO> GetEmployeeTransferDropdownData(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        return await _dapper.ExecuteStoredProcedureMultipleAsync(
            "usp_GetDropdownDataForEmployeeTransfer",
            parameters,
            async multi =>
            {
                EmployeeTransferDropdownsDTO? dropdownsData = new EmployeeTransferDropdownsDTO
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
                    Employees = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                    DesignationCodes = (await multi.ReadAsync<DesignationCodeDto>()).ToList(),
                    OccupationalStatuses = (await multi.ReadAsync<OccupationalStatusDto>()).ToList(),
                    AppointmentTypes = (await multi.ReadAsync<AppointmentTypeDto>()).ToList(),
                    Designations = (await multi.ReadAsync<DesignationDto>()).ToList(),
                    ReportToEmployees = (await multi.ReadAsync<DropDownViewModel>()).ToList()
                };
                return dropdownsData;
            }
        );
    }

    public async Task<EmployeeTransferAutoFillDTO?> GetEmployeeAutoFillData(long employeeId, long companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@employee_id", employeeId);
        parameters.Add("@company_id", companyId);

        var result = await _dapper.ExecuteStoredProcedureSingle<EmployeeTransferAutoFillDTO>(
            "usp_GetEmployeeTransferAutofilldata",
            parameters
        );

        return result;
    }
    public async Task<EmployeeTransferDetailDTO?> AddEmployeeTransfer(EmployeeTransferRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@EmployeeInitialsSurname", request.EmployeeInitialsSurname);
        parameters.Add("@DesignationId", request.DesignationId);
        parameters.Add("@JobGradeId", request.JobGradeId);
        parameters.Add("@WSPCategoryId", request.WSPCategoryId);
        parameters.Add("@OFOCodeId", request.OFOCodeId);
        parameters.Add("@MajorCostCenterId", request.MajorCostCenterId);
        parameters.Add("@NICGradeId", request.NICGradeId);
        parameters.Add("@OccupationalCategoryId", request.OccupationalCategoryId);
        parameters.Add("@OccupationalLevelId", request.OccupationalLevelId);
        parameters.Add("@EffectiveDate", request.EffectiveDate);
        parameters.Add("@ReportToId", request.ReportToId);
        parameters.Add("@BranchId", request.BranchId);
        parameters.Add("@DepartmentId", request.DepartmentId);
        parameters.Add("@ProvinceId", request.ProvinceId);
        parameters.Add("@SupportFunctionId", request.SupportFunctionId);
        parameters.Add("@OccupationalStatusId", request.OccupationalStatusId);
        parameters.Add("@AppointmentTypeId", request.AppointmentTypeId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@UserId", request.UserId);

        return await _dapper.ExecuteStoredProcedureSingle<EmployeeTransferDetailDTO>("usp_AddEmployeeTransfer", parameters);
    }
    public async Task<List<EmployeeTransferDisplayDataModel>> GetEmployeeTransferList(long companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        var result = await _dapper.ExecuteStoredProcedure<EmployeeTransferDisplayDataModel>("usp_GetEmployeeTransferList", parameters);
        return result.ToList();
    }
}