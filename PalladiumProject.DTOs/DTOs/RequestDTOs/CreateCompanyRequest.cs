namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs
{
    public class CreateCompanyRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Country { get; set; } = 0;
        public int NoOfEmployee { get; set; } = 0;
        public string Password { get; set; } = null!;
        public long ContactNo { get; set; } = 0;
        public string Company { get; set; } = null!;
    }
}
