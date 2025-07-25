namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Admin
{
    public class UserListResponseDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AccessRoleName { get; set; } = string.Empty;
    }
}