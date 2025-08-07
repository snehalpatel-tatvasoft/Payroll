namespace PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTransfer;

public class EmployeeTransferRequestDTO
{
    public long? EmployeeTransferId { get; set; }
    public long EmployeeId { get; set; }
    public string EmployeeInitialsSurname { get; set; } = "";
    public long? DesignationId { get; set; }
    public long? JobGradeId { get; set; }
    public long? WSPCategoryId { get; set; }
    public long? OFOCodeId { get; set; }
    public long? MajorCostCenterId { get; set; }
    public long? NICGradeId { get; set; }
    public long? OccupationalCategoryId { get; set; }
    public long? OccupationalLevelId { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public long? ReportToId { get; set; }
    public long? BranchId { get; set; }
    public long? DepartmentId { get; set; }
    public long? ProvinceId { get; set; }
    public long? SupportFunctionId { get; set; }
    public long? OccupationalStatusId { get; set; }
    public long? AppointmentTypeId { get; set; }
    public long CompanyId { get; set; }
    public int UserId { get; set; } 
}
