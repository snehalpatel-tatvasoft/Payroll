namespace PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;


public class AccessRoleDTO
{
    public int AccessRoleId { get; set; }         
    public string AccessRoleName { get; set; } = "";
    public int? AccessTypeId { get; set; }        
    public long CompanyId { get; set; }           
    public string? UserId { get; set; }     
}