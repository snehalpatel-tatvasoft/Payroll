namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs
{
    public class CountryDropdownResponse
    {
        public string CountryId { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string? CountryCode { get; set; }
        public string? IssueCode { get; set; }
    }
}
