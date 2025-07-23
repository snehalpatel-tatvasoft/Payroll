using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
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
                    Employees = (await multi.ReadAsync<EmployeeDropdownDto>()).ToList(),
                    DesignationCodes = (await multi.ReadAsync<DesignationCodeDto>()).ToList(),
                    OccupationalStatuses = (await multi.ReadAsync<OccupationalStatusDto>()).ToList(),
                    AppointmentTypes = (await multi.ReadAsync<AppointmentTypeDto>()).ToList(),
                    Designations = (await multi.ReadAsync<DesignationDto>()).ToList(),
                    ReportToEmployees = (await multi.ReadAsync<EmployeeDropdownDto>()).ToList()
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

}