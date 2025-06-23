namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs
{
    public class CreateUserRequestDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public long ContactNo { get; set; } = 0;
        public int CompanyId { get; set; } = 0;
    }
}
