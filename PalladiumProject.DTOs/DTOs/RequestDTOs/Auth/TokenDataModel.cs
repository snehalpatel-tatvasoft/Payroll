namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth
{
    public class TokenDataModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }

    public class UserDataModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public int AccessTypeId { get; set; }
        public int AccessRoleId { get; set; }
    }

    public class LoginResposeModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsMultiUser { get; set; }
        public int? CompanyId { get; set; }
        public List<CompanyDetails>? CompanyDetails { get; set; }
    }

    public class ForgotResponseModel
    {
        public bool IsMultiUser { get; set; }
        public List<CompanyDetails>? CompanyDetails { get; set; }
    }
}
