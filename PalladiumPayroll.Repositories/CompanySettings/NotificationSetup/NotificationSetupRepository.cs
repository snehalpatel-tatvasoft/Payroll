using Dapper;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;
using System.Data;

namespace PalladiumPayroll.Repositories.CompanySettings
{
    public class NotificationSetupRepository : INotificationSetupRepository
    {
        private readonly DapperContext _dapper;

        public NotificationSetupRepository(DapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<List<NotificationTemplateResponseDTO>> GetNotificationTemplatesByCompanyId(long companyId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            List<NotificationTemplateResponseDTO>? result = await _dapper.ExecuteStoredProcedure<NotificationTemplateResponseDTO>(
                "usp_GetNotificationTemplatesByCompanyId", parameters);
            return result.ToList();
        }

        public async Task<List<NotificationTypeResponseDTO>> GetNotificationTypes()
        {
            List<NotificationTypeResponseDTO>? result = await _dapper.ExecuteStoredProcedure<NotificationTypeResponseDTO>(
                "usp_GetNotificationTypes", null);
            return result.ToList();
        }

        public async Task<List<EmployeeResponseDTO>> GetEmployeesByCompanyIdForNotification(long companyId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.Int64);

            List<EmployeeResponseDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeeResponseDTO>(
                "usp_GetEmployeesByCompanyIdForNotification", parameters);
            return result.ToList();
        }

        public async Task<int> CreateNotificationTemplate(NotificationTemplateRequestDTO request)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64);
            parameters.Add("@NotificationTypeId", request.NotificationTypeId, dbType: DbType.Int32);
            parameters.Add("@NotificationTemplateName", request.NotificationTemplateName, dbType: DbType.String);
            parameters.Add("@TimeToSend", request.TimeToSend, dbType: DbType.Int32);
            parameters.Add("@ManagerInBCC", request.ManagerInBCC, dbType: DbType.Boolean);
            parameters.Add("@EmailsInBCC", request.EmailsInBCC, dbType: DbType.String);
            parameters.Add("@EmailTemplate", request.EmailTemplate, dbType: DbType.String);
            parameters.Add("@CreatedBy", request.CreatedBy, dbType: DbType.String);
            parameters.Add("@CreatedDate", DateTime.UtcNow, dbType: DbType.DateTime);
            parameters.Add("@SendTime", request.SendTime, dbType: DbType.String);
            parameters.Add("@Subject", request.Subject, dbType: DbType.String);
            parameters.Add("@EmployeeIds", request.EmployeeIds != null ? string.Join(",", request.EmployeeIds) : null, dbType: DbType.String);
            parameters.Add("@NotificationTemplateId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedure<NotificationTemplateRequestDTO>("usp_CreateNotificationTemplate", parameters);
            return parameters.Get<int>("@NotificationTemplateId");
        }

        public async Task<NotificationTemplateResponseDTO> GetNotificationTemplateById(int notificationTemplateId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@NotificationTemplateId", notificationTemplateId, dbType: DbType.Int32);

            List<NotificationTemplateResponseDTO>? result = await _dapper.ExecuteStoredProcedure<NotificationTemplateResponseDTO>(
               "usp_GetNotificationTemplateById", parameters);
            return result.FirstOrDefault();
        }

        public async Task<List<long>> GetEmployeesByNotificationTemplateId(int notificationTemplateId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@NotificationTemplateId", notificationTemplateId, dbType: DbType.Int32);

            List<long>? result = await _dapper.ExecuteStoredProcedure<long>(
                "usp_GetEmployeesByNotificationTemplateId", parameters);
            return result.ToList();
        }

        public async Task<int> UpdateNotificationTemplate(NotificationTemplateRequestDTO request)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@NotificationTemplateId", request.NotificationTemplateId, dbType: DbType.Int32);
            parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64);
            parameters.Add("@NotificationTypeId", request.NotificationTypeId, dbType: DbType.Int32);
            parameters.Add("@NotificationTemplateName", request.NotificationTemplateName, dbType: DbType.String);
            parameters.Add("@TimeToSend", request.TimeToSend, dbType: DbType.Int32);
            parameters.Add("@ManagerInBCC", request.ManagerInBCC, dbType: DbType.Boolean);
            parameters.Add("@EmailsInBCC", request.EmailsInBCC, dbType: DbType.String);
            parameters.Add("@EmailTemplate", request.EmailTemplate, dbType: DbType.String);
            parameters.Add("@UpdatedBy", request.CreatedBy, dbType: DbType.String);
            parameters.Add("@UpdatedDate", DateTime.UtcNow, dbType: DbType.DateTime);
            parameters.Add("@SendTime", request.SendTime, dbType: DbType.String);
            parameters.Add("@Subject", request.Subject, dbType: DbType.String);
            parameters.Add("@EmployeeIds", request.EmployeeIds != null ? string.Join(",", request.EmployeeIds) : null, dbType: DbType.String);

            await _dapper.ExecuteStoredProcedure<NotificationTemplateRequestDTO>("usp_UpdateNotificationTemplate", parameters);
            return request.NotificationTemplateId;
        }

        public async Task<int> DeleteNotificationTemplate(int notificationTemplateId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@NotificationTemplateId", notificationTemplateId, dbType: DbType.Int32);

            await _dapper.ExecuteStoredProcedure<object>("usp_DeleteNotificationTemplate", parameters);
            return notificationTemplateId;
        }
    }
}