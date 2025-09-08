namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class ResetPasswordRequest
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }

    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class CompanyDetails
    {
        public Guid UserId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}
