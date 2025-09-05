namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int AccessRoleID { get; set; }
        public string PasswordHash { get; set; } = null!;
        public int RoleId { get; set; }
        public bool ConfirmedEmail { get; set; }
        public string CompanyName { get; set; } = null!;
        public int CompanyId { get; set; }
    }
}
