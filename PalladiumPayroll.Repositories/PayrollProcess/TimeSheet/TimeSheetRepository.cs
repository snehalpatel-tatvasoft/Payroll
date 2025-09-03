using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.TimeSheet;
using System.Data;

namespace PalladiumPayroll.Services.PayrollProcess.TimeSheet
{
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly DapperContext _dapper;

        public TimeSheetRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

        public async Task<List<TimeSheetImport>> GetLatestImportedData(int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            return await _dapper.ExecuteStoredProcedure<TimeSheetImport>("usp_GetLatestImportTimeSheet", parameters);
        }

        public async Task<bool> ImportTimeSheetData(DataTable timeSheetDataTable)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TimeSheetRecord", timeSheetDataTable.AsTableValuedParameter("TimeSheetImportType"));
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_AddTimeSheetImport", parameters);
        }

        public async Task<bool> ProcessTimeSheet()
        {
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_ProcessTimeSheet");
        }
    }
}
