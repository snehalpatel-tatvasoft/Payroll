using Dapper;
using Microsoft.AspNetCore.Http;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.HRFunctions.DisciplinaryLog;
using System.Data;

namespace PalladiumPayroll.Repositories.HRFunctions.DisciplinaryLog
{
    public class DisciplinaryLogRepository : IDisciplinaryLogRepository
    {
        private readonly DapperContext _dapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DisciplinaryLogRepository(DapperContext dapper,IHttpContextAccessor httpContextAccessor)
        {
            _dapper = dapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DisciplinaryLogResponseDTO>> GetDisciplinaryLogByCompanyId(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<DisciplinaryLogResponseDTO>(
                "usp_GetDisciplinaryLogByCompanyId", parameters);
            return result.ToList();
        }

        public async Task<List<EmployeeDropdownDTO>> GetEmployeesForDisciplinaryLogDropdown(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<EmployeeDropdownDTO>(
                "usp_GetEmployeesForDisciplinaryLogDropdown", parameters);
            return result.ToList();
        }

        public async Task<bool> UpsertDisciplinaryLog(DisciplinaryLogUpsertDTO request)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@DisciplinaryLogId", request.DisciplinaryLogId);
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@CompanyId", request.CompanyId);
            parameters.Add("@ActionDate", request.ActionDate);
            parameters.Add("@Representative", request.Representative);
            parameters.Add("@Charge", request.Charge);
            parameters.Add("@Witnesses", request.Witnesses);
            parameters.Add("@Result", request.Result);
            parameters.Add("@FilePath", request.FilePath);
            parameters.Add("@FileName", request.FileName);
            parameters.Add("@FileType", request.FileType);
            parameters.Add("@FileSize", request.FileSize);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

            parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertDisciplinaryLog", parameters);

            return parameters.Get<bool>("@IsSuccess");
        }


        // public async Task<long> CreateDisciplinaryLog(DisciplinaryLogRequestDTO disciplinaryLog)
        // {
        //     var parameters = new DynamicParameters();
        //     parameters.Add("@EmployeeId", disciplinaryLog.EmployeeId, dbType: DbType.Int64);
        //     parameters.Add("@CompanyId", disciplinaryLog.CompanyId, dbType: DbType.Int64);
        //     parameters.Add("@ActionDate", disciplinaryLog.ActionDate, dbType: DbType.DateTime);
        //     parameters.Add("@Representative", disciplinaryLog.Representative, dbType: DbType.String);
        //     parameters.Add("@Charge", disciplinaryLog.Charge, dbType: DbType.String);
        //     parameters.Add("@Witnesses", disciplinaryLog.Witnesses, dbType: DbType.String);
        //     parameters.Add("@Result", disciplinaryLog.Result, dbType: DbType.String);
        //     parameters.Add("@FileName", disciplinaryLog.FileName, dbType: DbType.String);
        //     parameters.Add("@FilePath", disciplinaryLog.FilePath, dbType: DbType.String);
        //     parameters.Add("@CreatedBy", disciplinaryLog.CreatedBy, dbType: DbType.String);

        //     var result = await _dapper.ExecuteStoredProcedure<long>(
        //         "usp_CreateDisciplinaryLog", parameters);
        //     return result.FirstOrDefault();
        // }

        public async Task<DisciplinaryLogByIdResponseDTO> GetDisciplinaryLogById(long disciplinaryLogId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DisciplinaryLogId", disciplinaryLogId, dbType: DbType.Int64);

            var result = await _dapper.ExecuteStoredProcedure<DisciplinaryLogByIdResponseDTO>(
                "usp_GetDisciplinaryLogById", parameters);
            return result.FirstOrDefault();
        }

        // public async Task<int> UpdateDisciplinaryLog(DisciplinaryLogEditRequestDTO disciplinaryLog)
        // {
        //     var parameters = new DynamicParameters();
        //     parameters.Add("@DisciplinaryLogId", disciplinaryLog.DisciplinaryLogId, dbType: DbType.Int64);
        //     parameters.Add("@EmployeeId", disciplinaryLog.EmployeeId, dbType: DbType.Int64);
        //     parameters.Add("@CompanyId", disciplinaryLog.CompanyId, dbType: DbType.Int64);
        //     parameters.Add("@ActionDate", disciplinaryLog.ActionDate, dbType: DbType.DateTime);
        //     parameters.Add("@Representative", disciplinaryLog.Representative, dbType: DbType.String);
        //     parameters.Add("@Charge", disciplinaryLog.Charge, dbType: DbType.String);
        //     parameters.Add("@Witnesses", disciplinaryLog.Witnesses, dbType: DbType.String);
        //     parameters.Add("@Result", disciplinaryLog.Result, dbType: DbType.String);
        //     parameters.Add("@FileName", disciplinaryLog.FileName, dbType: DbType.String);
        //     parameters.Add("@FilePath", disciplinaryLog.FilePath, dbType: DbType.String);
        //     parameters.Add("@UpdatedBy", disciplinaryLog.UpdatedBy, dbType: DbType.String);
        //     parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //     var result = await _dapper.ExecuteStoredProcedure<int>(
        //         "usp_UpdateDisciplinaryLog", parameters);
        //     return parameters.Get<int>("@RowsAffected");
        // }

        public async Task<int> DeleteDisciplinaryLog(long disciplinaryLogId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DisciplinaryLogId", disciplinaryLogId, dbType: DbType.Int64);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _dapper.ExecuteStoredProcedure<int>(
                "usp_DeleteDisciplinaryLog", parameters);
            return parameters.Get<int>("@RowsAffected");
        }
    }
}