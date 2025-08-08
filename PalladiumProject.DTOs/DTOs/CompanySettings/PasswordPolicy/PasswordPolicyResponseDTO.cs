namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings
{
    public class PasswordPolicyResponseDTO
    {
        public long PasswordPolicyId { get; set; }
        public string PolicyName { get; set; } = string.Empty;
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public int NoOfUppercaseLetters { get; set; }
        public int NoOfDigits { get; set; }
        public int NoOfSpecialLetters { get; set; }
        public int PasswordAgeInterval { get; set; }
        public int SessionTimeOutInterval { get; set; }
        public bool? IsInactive { get; set; }
        public bool? IsApplied { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? ModifiedBy { get; set; }
        public long CompanyId { get; set; }
    }
}