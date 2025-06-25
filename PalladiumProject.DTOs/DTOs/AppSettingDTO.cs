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

    public class SmtpSettings
    {
        public string SMTPMailServer { get; set; } = null!;
        public int SMTPPort { get; set; } = 587;
        public string SMTPMailUser { get; set; } = null!;
        public string SMTPMailPassword { get; set; } = null!;
        public bool SMTPEnableSsl { get; set; }
        public string SMTPFrom { get; set; } = null!;
    }
}
