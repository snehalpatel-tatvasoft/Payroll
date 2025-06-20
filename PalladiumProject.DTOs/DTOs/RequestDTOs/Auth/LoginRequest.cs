namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
