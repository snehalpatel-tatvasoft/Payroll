using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous.Constants;
using PalladiumPayroll.Helper;
using System.Data;

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

        public async Task<List<BatchData>> GetExistingBatchList(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            return await _dapper.ExecuteStoredProcedure<BatchData>("usp_GetExistingBatchPayslipList", parameters);
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

        public async Task<int> BatchPayslipTransactionDetailInsert(BatchPayslipInsert reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@BatchDescription", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.CycleId);
            parameters.Add("@ProcessPriod", reqModel.ProcessPriod);
            parameters.Add("@IsRecurring", reqModel.IsRecurring);
            parameters.Add("@IsSpecialRun", reqModel.TransactionType == (int)PayslipTransactionType.SpecialRun);
            parameters.Add("@IsLeavePay", reqModel.TransactionType == (int)PayslipTransactionType.LeavePay);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            return await _dapper.ExecuteStoredProcedureFirst<int>("BatchPayslipTransactionDetailsInsert", parameters);
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


        #region MultiTransaction
        public async Task<List<SpecialTransaction>> GetSpecialRunTransaction(MultiTransactionGet reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProcessPreiodId", reqModel.ProcessPreiodId);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@TransactionType", reqModel.TransactionType);
            return await _dapper.ExecuteStoredProcedure<SpecialTransaction>("SP_GetSpecialRunTransactions", parameters);
        } 

        public async Task<List<BatchPayslipTransaction>> BatchTransactionUpsertBulk(BatchPayslipBulkInsert reqModel)
        {
            var empTbl = new DataTable();
            empTbl.Columns.Add("Employee", typeof(int));
            foreach (var empId in reqModel.EmployeeIds)
            {
                empTbl.Rows.Add(empId);
            }

            var transTbl = new DataTable();
            transTbl.Columns.Add("BatchId", typeof(int));
            transTbl.Columns.Add("BatchName", typeof(string));
            transTbl.Columns.Add("Description", typeof(string));
            transTbl.Columns.Add("CycleId", typeof(int));
            transTbl.Columns.Add("ProcessPreiodId", typeof(int));
            transTbl.Columns.Add("TransactionName", typeof(string));
            transTbl.Columns.Add("Amount", typeof(decimal));
            transTbl.Columns.Add("Hours", typeof(decimal));
            transTbl.Columns.Add("IsRecuring", typeof(bool));
            foreach (var transaction in reqModel.BatchTransaction)
            {
                transTbl.Rows.Add(reqModel.BatchId, reqModel.BatchNumber, reqModel.BatchDescription, reqModel.CycleId, reqModel.ProcessPriod, transaction.TransactionName, transaction.Amount, transaction.Hours, transaction.IsRecurring);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@BatchDescription", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.CycleId);
            parameters.Add("@ProcessPriod", reqModel.ProcessPriod);
            parameters.Add("@IsRecurring", reqModel.IsRecurring);
            parameters.Add("@tblEmployees", empTbl.AsTableValuedParameter("dbo.tblEmployee"));
            parameters.Add("@tblBatchTransaction", transTbl.AsTableValuedParameter("dbo.tblBatchTransaction"));
            parameters.Add("@IsSpecialRun", reqModel.TransactionType == (int)PayslipTransactionType.SpecialRun);
            parameters.Add("@IsLeavePay", reqModel.TransactionType == (int)PayslipTransactionType.LeavePay);
            return await _dapper.ExecuteStoredProcedure<BatchPayslipTransaction>("BatchPayslipTransactionDetailsInsertORUpdate", parameters);
        }

        public async Task<bool> BatchTransactionDeleteBulk(BatchPayslipBulkInsert reqModel)
        {
            var empTbl = new DataTable();
            empTbl.Columns.Add("Employee", typeof(int));
            foreach (var empId in reqModel.EmployeeIds)
            {
                empTbl.Rows.Add(empId);
            }
            var transTbl = new DataTable();
            transTbl.Columns.Add("BatchId", typeof(int));
            transTbl.Columns.Add("BatchName", typeof(string));
            transTbl.Columns.Add("Description", typeof(string));
            transTbl.Columns.Add("CycleId", typeof(int));
            transTbl.Columns.Add("ProcessPreiodId", typeof(int));
            transTbl.Columns.Add("TransactionName", typeof(string));
            transTbl.Columns.Add("Amount", typeof(decimal));
            transTbl.Columns.Add("Hours", typeof(decimal));
            transTbl.Columns.Add("IsRecuring", typeof(bool));
            foreach (var transaction in reqModel.BatchTransaction)
            {
                transTbl.Rows.Add(reqModel.BatchId, reqModel.BatchNumber, reqModel.BatchDescription, reqModel.CycleId, reqModel.ProcessPriod, transaction.TransactionName, transaction.Amount, transaction.Hours, transaction.IsRecurring);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@BatchDescription", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.CycleId);
            parameters.Add("@ProcessPriod", reqModel.ProcessPriod);
            parameters.Add("@tblEmployees", empTbl.AsTableValuedParameter("dbo.tblEmployee"));
            parameters.Add("@tblBatchTransaction", transTbl.AsTableValuedParameter("dbo.tblBatchTransaction"));
            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("BatchPayslipTransactionDetailsDelete", parameters);
            return result;
        }
        #endregion

        public async Task<BatchPayslipTransaction?> UpdateTransactionDetail(BatchTransactionUpdate reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchTransactionId", reqModel.BatchTransactionId);
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@TransactionType", reqModel.TransactionType);
            parameters.Add("@TransactionName", reqModel.TransactionName);
            parameters.Add("@Unit", reqModel.Unit);
            parameters.Add("@IsRecurring", reqModel.IsRecurring ?? false);
            parameters.Add("@TransactionValues", reqModel.TransactionValues);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            return await _dapper.ExecuteStoredProcedureSingle<BatchPayslipTransaction>("SP_UpadteTransactionDetailsForBulk", parameters);
        }

        public async Task<bool> DeleteBatchTransaction(long transactionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", transactionId);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("SP_DeleteTransactionDetails", parameters);
        }

        public async Task<int> SaveBatchPayslip(BatchPayslipInsert reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@BatchDescription", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.CycleId);
            parameters.Add("@ProcessPriod", reqModel.ProcessPriod);
            parameters.Add("@IsRecurring", reqModel.IsRecurring);
            parameters.Add("@IsSpecialRun", reqModel.TransactionType == (int)PayslipTransactionType.SpecialRun);
            return await _dapper.ExecuteStoredProcedureSingle<int>("SP_SaveActualBatchPayslip", parameters); 
        }

        public async Task<SPResultMessage> ProcessBatchPayslip(BatchPayslipProcess reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@IsAppend", reqModel.IsAppend);
            parameters.Add("@PayslipType", reqModel.TransactionType);
            return await _dapper.ExecuteStoredProcedureFirst<SPResultMessage>("BatchPayslipProcess", parameters);
        }
    }
}
