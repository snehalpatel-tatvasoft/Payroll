namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Admin
{
    public class UserListResponseDTO
    {
        public Guid Id { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AccessRoleName { get; set; } = string.Empty;
    }

    public class AccessRoleResponseDTO
    {
        public int AccessRoleId { get; set; }
        public string AccessRoleName { get; set; } = string.Empty;
    }
}