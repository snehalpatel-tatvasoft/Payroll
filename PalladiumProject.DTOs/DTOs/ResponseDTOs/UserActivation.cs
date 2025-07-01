namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs
{
    public class UserActivation
    {
        public string? CompanyId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int MaximumEmployees { get; set; }
        public int NoOfEmployees { get; set; }
        public int RemainingDays { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime LicenseExpirationDate { get; set; }

    }
}
