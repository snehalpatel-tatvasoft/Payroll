namespace PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;


public class AccessRoleResponseDTO
{
    public int AccessRoleId { get; set; }         
    public string? AccessRoleName { get; set; } 
    public int? AccessTypeId { get; set; }        
    public long CompanyId { get; set; }    
}