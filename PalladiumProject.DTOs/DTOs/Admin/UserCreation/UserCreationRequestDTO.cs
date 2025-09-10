namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin
{
    public class UserCreationRequestDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public int AccessRoleId { get; set; }
        public string ContactNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public long CompanyId { get; set; }
    }

    public class SaveUserFunctionalityAccessRightsDTO
    {
        public Guid UserId { get; set; }
        public int FunctionalityId { get; set; }
        public bool View { get; set; }
        public bool Edit { get; set; }
        public bool New { get; set; }
        public bool Delete { get; set; }
    }

    public class SaveUserPayFrequencyAccessRightsDTO
    {
        public Guid UserId { get; set; }
        public long CompanyPayrollId { get; set; }
        public bool IsAllow { get; set; }
    }

    public class ChangePasswordModel
    {
        public string Password { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }

    public class ChangeEmailModel
    {
        public string Email { get; set; } = null!;
        public string NewEmail { get; set; } = null!;
    }
}