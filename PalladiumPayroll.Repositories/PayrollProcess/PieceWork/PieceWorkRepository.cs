using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;

namespace PalladiumPayroll.Repositories.PayrollProcess.PieceWork;

public class PieceWorkRepository : IPieceWorkRepository
{
    private readonly DapperContext _dapper;

    public PieceWorkRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<PieceWorkDropdownsDTO> GetPieceWorkDropdownData(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        return await _dapper.ExecuteStoredProcedureMultipleAsync(
            "usp_GetDropdownDataForPieceWork",
            parameters,
            async multi =>
            {
                PieceWorkDropdownsDTO? dropdownsData = new PieceWorkDropdownsDTO
                {
                    ProductTypes = (await multi.ReadAsync<ProductTypeDto>()).ToList(),
                    Units = (await multi.ReadAsync<UnitDto>()).ToList(),
                    Areas = (await multi.ReadAsync<AreaDto>()).ToList(),
                    Employees = (await multi.ReadAsync<EmployeeDto>()).ToList(),
                };
                return dropdownsData;
            }
        );
    }

    public async Task<List<DropDownViewModel>> AddPieceWorkDropdownItem(PieceWorkDropdownItem reqItem)
    {
        List<DropDownViewModel>? result = new List<DropDownViewModel>();
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@Name", reqItem.Name);
        parameters.Add("@CompanyId", reqItem.CompanyId);

        switch (reqItem.Type)
        {
            case 1:
                result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddPieceWorkProductType", parameters);
                break;
            case 2:
                result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddPieceWorkUnit", parameters);
                break;
            case 3:
                result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddPieceWorkArea", parameters);
                break;
        }
        return result;
    }

    public async Task<DeleteDropDownResult> DeletePieceWorkDropdownItem(int id, int type)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@Type", type);

        DeleteDropDownResult? result = await _dapper.ExecuteStoredProcedureSingle<DeleteDropDownResult>("usp_DeletePieceWorkDropdownItem", parameters);

        if (result == null)
        {
            return new DeleteDropDownResult
            {
                Success = false,
                Message = "No response from stored procedure."
            };
        }
        return result;
    }


     public async Task<bool> UpsertPieceWorkMasterData(UpsertPieceworkMasterDataDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@ByUnitOrEmployee", request.ByUnitOrEmployee);
        parameters.Add("@ProductTypeId", request.ProductTypeId);
        parameters.Add("@AreaId", request.AreaId);
        parameters.Add("@UnitId", request.UnitId);
        parameters.Add("@Rate", request.Rate);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@EmployeeId", request.EmployeeId);

        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertPieceworkMasterData", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }

}
