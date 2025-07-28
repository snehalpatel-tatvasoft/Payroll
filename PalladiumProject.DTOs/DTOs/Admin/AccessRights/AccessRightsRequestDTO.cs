namespace PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;


public class AccessRoleDTO
{
    public int AccessRoleId { get; set; }         
    public string AccessRoleName { get; set; } = "";
    public int? AccessTypeId { get; set; }        
    public long CompanyId { get; set; }           
    public string? UserId { get; set; }     
}

public class SaveEmployeeAccessRightsDTO
{
    public int AccessRoleId { get; set; }
    public int FunctionalityId { get; set; }
    public bool View { get; set; }
    public bool Edit { get; set; }
    public bool Delete { get; set; }
}


public class SavePayFrequencyAccessRightsDTO
{
    public int AccessRoleId { get; set; }
    public long CompanyPayrollId { get; set; }
    public bool IsAllow { get; set; }
    public string UserId { get; set; } = string.Empty;
}