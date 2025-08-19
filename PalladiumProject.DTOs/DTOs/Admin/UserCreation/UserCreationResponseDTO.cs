namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Admin
{
    public class UserListResponseDTO
    {
        public Guid Id { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AccessRoleName { get; set; } = string.Empty;
        public int TypeId { get; set; }
    }

    public class AccessRoleResponseDTO
    {
        public int AccessRoleId { get; set; }
        public string AccessRoleName { get; set; } = string.Empty;
    }

    public class UserFunctionalityAccessRightsDTO
    {
        public long UserFunctionalityId { get; set; }
        public int FunctionalityId { get; set; }
        public string FunctionalityName { get; set; } = string.Empty;
        public bool View { get; set; }
        public bool Edit { get; set; }
        public bool New { get; set; }
        public bool Delete { get; set; }
        public bool IsActive { get; set; }
    }


    public class UserPayFrequencyAccessRightsDTO
    {
        public long UserPayFrequencyId { get; set; }
        public long CompanyPayrollId { get; set; }
        public string PayrollCycleName { get; set; } = string.Empty;
        public bool IsAllow { get; set; }
    }
}