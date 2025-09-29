using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous.Constants;

namespace PalladiumPayroll.Repositories.PayrollProcess.BatchPayslip
{
    public class BatchPayslipRepository : IBatchPayslipRepository
    {
        private readonly DapperContext _dapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BatchPayslipRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dapper = new DapperContext(configuration);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> UpdateBatchDetail(BatchInfoRequest reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@Description", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.PayrollCycle);
            parameters.Add("@PeriodId", reqModel.ProcessPeriod);
            parameters.Add("@IsRecurring", reqModel.IsRecurring);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<int>("usp_UpdateBatchPayslip", parameters);
        }

        public async Task<List<BatchPayslipTransaction>> LoadPayslipTransaction(int batchId, bool mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", batchId);
            parameters.Add("@Mode", mode);
            return await _dapper.ExecuteStoredProcedure<BatchPayslipTransaction>("usp_LoadBatchPayslipTransaction", parameters);
        }

        public async Task<List<BatchPayslipLeave>> LoadPayslipEmployeeLeave(int batchId, bool mode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", batchId);
            parameters.Add("@Mode", mode);
            return await _dapper.ExecuteStoredProcedure<BatchPayslipLeave>("usp_BatchLoadEmployeesLeaveDetails", parameters);
        }
    }
}
