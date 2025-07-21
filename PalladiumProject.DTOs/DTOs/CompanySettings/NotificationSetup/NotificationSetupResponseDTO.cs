namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings
{
    public class NotificationTemplateResponseDTO
    {
        public long NotificationTemplateId { get; set; }
        public long? CompanyId { get; set; }
        public int? NotificationTypeId { get; set; }
        public string NotificationTemplateName { get; set; }
        public int? TimeToSend { get; set; }
        public bool? ManagerInBCC { get; set; }
        public string EmailsInBCC { get; set; }
        public string EmailTemplate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SendTime { get; set; }
        public string Subject { get; set; }
        public string NotificationTypeName { get; set; } 
    }

    public class NotificationTypeResponseDTO
    {
        public int NotificationTypeId { get; set; }
        public string NotificationTypeName { get; set; }
    }

    public class EmployeeResponseDTO
    {
        public long EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public string DepartmentName { get; set; }
        public string HomeLanguageName { get; set; }
    }
}