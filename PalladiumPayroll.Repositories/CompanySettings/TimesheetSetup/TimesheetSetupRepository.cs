using PalladiumPayroll.DataContext;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Repositories.CompanySettings
{
    public class TimesheetSetupRepository : ITimesheetSetupRepository
    {
        private readonly DapperContext _dapper;
        private readonly IConfiguration _configuration;

        public TimesheetSetupRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _dapper = new DapperContext(_configuration);
        }

        public async Task<List<TimesheetSetupResponseDTO>> GetPayrollCycles(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<TimesheetSetupResponseDTO>(
                "sp_GetPayrollCycles", parameters);
            return result.ToList();
        }

        public async Task<List<TimesheetPayrollSetupResponseDTO>> GetTimesheetPayrollSetup(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<TimesheetPayrollSetupResponseDTO>(
                "sp_GetTimesheetPayrollSetup", parameters);
            return result.ToList();
        }

        public async Task<bool> UpsertTimesheetPayrollSetup(TimesheetSetupRequestDTO request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64);
            parameters.Add("@IsClockCard", request.IsClockCard, dbType: DbType.Boolean);
            parameters.Add("@CompanyPayrollIds", string.Join(",", request.CompanyPayrollIds));

            await _dapper.ExecuteStoredProcedure<object>(
                "sp_UpsertTimesheetPayrollSetup", parameters);
            return true;
        }
    }
}