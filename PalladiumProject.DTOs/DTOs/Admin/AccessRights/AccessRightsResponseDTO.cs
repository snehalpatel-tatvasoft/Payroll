namespace PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;


public class AccessRoleResponseDTO
{
    public int AccessRoleId { get; set; }         
    public string? AccessRoleName { get; set; } 
    public int? AccessTypeId { get; set; }        
    public long CompanyId { get; set; }    
}   


public class AccessRightsByRoleTypeDTO
{
    public int FunctionalityId { get; set; }
    public string FunctionalityName { get; set; } = string.Empty;
    public bool View { get; set; }
    public bool Edit { get; set; }
    public bool New { get; set; }
    public bool Delete { get; set; }
}


public class PayFrequencyAccessRightsDTO
{
    public int RolePayFrequenciesId { get; set; }
    public long CompanyPayrollId { get; set; }
    public string PayrollCycleName { get; set; } = string.Empty;
    public bool IsAllow { get; set; }
}