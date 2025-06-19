namespace PalladiumPayroll.DTOs.DTOs
{
    public class EncryptionSetting
    {
        public bool Enable { get; set; }
        public string Key { get; set; }
        public string IV { get; set; }
    }

    public class JwtSettings
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience {  get; set; }
    }

}
