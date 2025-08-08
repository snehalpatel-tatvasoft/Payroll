namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings
{
    public class PasswordPolicyRequestDTO
    {
        public string PolicyName { get; set; } = string.Empty;
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public int NoOfUppercaseLetters { get; set; }
        public int NoOfDigits { get; set; }
        public int NoOfSpecialLetters { get; set; }
        public int PasswordAgeInterval { get; set; }
        public int SessionTimeOutInterval { get; set; }
        public bool IsInactive { get; set; }
        public bool IsApplied { get; set; }
        public long CompanyId { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
    }
}