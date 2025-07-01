namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class CompanyDetails
    {
        public string CompanyName { get; set; } = string.Empty;
        public int AccessRoleID { get; set; }
    }
}
