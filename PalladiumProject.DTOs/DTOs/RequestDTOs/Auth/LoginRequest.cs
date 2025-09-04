namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class CompanyDetails
    {
        public int CompanyId { get; set; } = 0;
        public string CompanyName { get; set; } = string.Empty;
        public int AccessTypeId { get; set; }
    }
}
