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

        public async Task<BatchInfoWithBatchData> GetBatchInfo(int BatchId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", BatchId);
            BatchInfoWithBatchData batchInfoData = new BatchInfoWithBatchData();
            return await _dapper.ExecuteStoredProcedureMultipleAsync("usp_PayslipBatchInfo", parameters, async (multi) =>
            {
                batchInfoData.BatchInfo = await multi.ReadFirstAsync<BatchPayslipInsert>();
                batchInfoData.PeriodList = (await multi.ReadAsync<DropDownViewModel>()).ToList();
                batchInfoData.PayslipTransactions = (await multi.ReadAsync<BatchPayslipTransaction>()).ToList();
                batchInfoData.PayslipLeaves = (await multi.ReadAsync<BatchPayslipLeave>()).ToList();
                return batchInfoData;
            });
        }

        public async Task<bool> DeleteExistingBatch(int batchId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", batchId);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteBatchForPayslip", parameters);
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
            parameters.Add("@CycleId", reqModel.PayrollCycle);
            parameters.Add("@ProcessPriod", reqModel.ProcessPeriod);
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

        public async Task<List<DropDownViewModel>> GetEmployeeBaseOnPeriod(int periodId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProcessPeriodId", periodId);
            return await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_LoadEmployeesBasedOnPeroid", parameters);
        }

        public async Task<MultiTransaction> GetMultiTransaction(MultiTransactionGet reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProcessPreiodId", reqModel.ProcessPeriod);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@TransactionType", reqModel.TransactionType);
            List<DropDownViewModel> employeeList = await GetEmployeeBaseOnPeriod(reqModel.ProcessPeriod);
            List<SpecialTransaction> transaction = await _dapper.ExecuteStoredProcedure<SpecialTransaction>("SP_GetSpecialRunTransactions", parameters);
            return new MultiTransaction() { Employees = employeeList, Transactions = transaction };
        }

        public async Task<int> BatchTransactionUpsertBulk(BatchPayslipBulkInsert reqModel)
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
                transTbl.Rows.Add(reqModel.BatchId, reqModel.BatchNumber, reqModel.BatchDescription, reqModel.PayrollCycle, reqModel.ProcessPeriod, transaction.TransactionName, transaction.Amount, transaction.Hours, transaction.IsRecurring);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@BatchDescription", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.PayrollCycle);
            parameters.Add("@ProcessPriod", reqModel.ProcessPeriod);
            parameters.Add("@IsRecurring", reqModel.IsRecurring);
            parameters.Add("@tblEmployees", empTbl.AsTableValuedParameter("dbo.tblEmployee"));
            parameters.Add("@tblBatchTransaction", transTbl.AsTableValuedParameter("dbo.tblBatchTransaction"));
            parameters.Add("@IsSpecialRun", reqModel.TransactionType == (int)PayslipTransactionType.SpecialRun);
            parameters.Add("@IsLeavePay", reqModel.TransactionType == (int)PayslipTransactionType.LeavePay);
            return await _dapper.ExecuteStoredProcedureSingle<int>("BatchPayslipTransactionDetailsInsertORUpdate", parameters);
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
                transTbl.Rows.Add(reqModel.BatchId, reqModel.BatchNumber, reqModel.BatchDescription, reqModel.PayrollCycle, reqModel.ProcessPeriod, transaction.TransactionName, transaction.Amount, transaction.Hours, transaction.IsRecurring);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@BatchDescription", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.PayrollCycle);
            parameters.Add("@ProcessPriod", reqModel.ProcessPeriod);
            parameters.Add("@tblEmployees", empTbl.AsTableValuedParameter("dbo.tblEmployee"));
            parameters.Add("@tblBatchTransaction", transTbl.AsTableValuedParameter("dbo.tblBatchTransaction"));
            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("BatchPayslipTransactionDetailsDelete", parameters);
            return result;
        }

        public async Task<int> ImportBatchTransactionUpsert(BatchPayslipInsert reqModel, DataTable importTransactionData, string fileName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@BatchDescription", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.PayrollCycle);
            parameters.Add("@ProcessPriod", reqModel.ProcessPeriod);
            parameters.Add("@IsRecurring", reqModel.IsRecurring);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@TemplateName", "Batch Transaction");
            parameters.Add("@ImportFileName", fileName);
            parameters.Add("@ActualTableName", "BatchPayslipTransaction");
            parameters.Add("@tblMultiTrancsaction", importTransactionData.AsTableValuedParameter("dbo.[ImportMultiTransaction]"));
            var result = await _dapper.ExecuteStoredProcedureSingle<int>("SP_ImportMultiTransactions", parameters);
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

        public async Task<SPResultMessage> UpdateEmployeeLeaveDetail(PayslipLeave payslipLeave)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", payslipLeave.Id);
            parameters.Add("@EmployeeId", payslipLeave.EmployeeId);
            parameters.Add("@LeaveType", payslipLeave.LeaveType);
            parameters.Add("@FromDate", payslipLeave.FromDate);
            parameters.Add("@ToDate", payslipLeave.ToDate);
            parameters.Add("@Duration", payslipLeave.Duration);
            parameters.Add("@UnPaidLeave", payslipLeave.UnPaidLeave);
            parameters.Add("@LeaveStatusId",payslipLeave.LeaveStatusId);
            parameters.Add("@Comment", payslipLeave.Comment);
            return await _dapper.ExecuteStoredProcedureFirst<SPResultMessage>("usp_BatchUpadteEmployeeLeaveDetailsPreview", parameters);
        }

        public async Task<int> SaveBatchPayslip(BatchPayslipInsert reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@BatchDescription", reqModel.BatchDescription);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@CycleId", reqModel.PayrollCycle);
            parameters.Add("@ProcessPriod", reqModel.ProcessPeriod);
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
