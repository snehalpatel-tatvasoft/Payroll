using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

namespace PalladiumPayroll.Repositories.Utilities.DataImport;

public class DataImportRepository : IDataImportRepository
{
    private readonly DapperContext _dapper;

    public DataImportRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

   public async Task<TableDataModel<PayrollProcessingTransactionDto>> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", reqModel.CompanyId);
        parameters.Add("@CurrentPage", reqModel.CurrentPage);
        parameters.Add("@PageSize", reqModel.PageSize);
        parameters.Add("@SortBy", reqModel.SortBy ?? "PayrollProcessId");
        parameters.Add("@SortType", reqModel.SortType ? "ASC" : "DESC");
        parameters.Add("@Search", reqModel.Search ?? "");
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var data = await _dapper.ExecuteStoredProcedure<PayrollProcessingTransactionDto>("usp_GetPayrollProcessingTransactionsByCompany", parameters);
        var total = parameters.Get<int>("@TotalCount");

        return new TableDataModel<PayrollProcessingTransactionDto>
        {
            DataList = data,
            TotalCount = total
        };
    }

}
