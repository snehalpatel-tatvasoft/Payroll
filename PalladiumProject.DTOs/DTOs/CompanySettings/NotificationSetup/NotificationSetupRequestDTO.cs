namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings
{
    public class NotificationTemplateRequestDTO
    {
        public long CompanyId { get; set; }
        public int NotificationTemplateId { get; set; }
        public int NotificationTypeId { get; set; }
        public string NotificationTemplateName { get; set; }
        public int TimeToSend { get; set; }
        public bool ManagerInBCC { get; set; }
        public string EmailsInBCC { get; set; }
        public string EmailTemplate { get; set; }
        public string CreatedBy { get; set; }
        public string SendTime { get; set; }
        public string Subject { get; set; }
        public List<long> EmployeeIds { get; set; }
    }
}