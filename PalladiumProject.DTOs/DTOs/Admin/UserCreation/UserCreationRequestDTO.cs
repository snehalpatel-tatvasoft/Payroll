namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin
{
    public class UserCreationRequestDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public int AccessRoleID { get; set; }
        public string ContactNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public long CompanyId { get; set; }
    }
}