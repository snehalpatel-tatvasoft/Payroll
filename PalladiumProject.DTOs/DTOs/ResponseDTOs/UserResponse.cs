namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AccessRoleID { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public bool ConfirmedEmail { get; set; }
        public string CompanyId { get; set; } = string.Empty;
    }
}
