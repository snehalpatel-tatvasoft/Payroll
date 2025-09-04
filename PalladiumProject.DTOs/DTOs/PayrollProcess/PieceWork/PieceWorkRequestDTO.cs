namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;

public class PieceWorkRequestDTO
{
}

public class PieceWorkDropdownItem
{
    public string Name { get; set; } = "";
    public long CompanyId { get; set; }
    public int Type { get; set; }
}

public class UpsertPieceworkMasterDataDTO{
    public string ByUnitOrEmployee { get; set; } =string.Empty;
    public int ProductTypeId { get; set; }
    public int AreaId { get; set; }
    public int UnitId { get; set; }
    public decimal Rate { get; set; }
    public long CompanyId { get; set; }
    public long? EmployeeId { get; set; }
}

public class GetPieceworkRateRequestDTO
{
    public string Mode { get; set; } = string.Empty;
    public long CompanyId { get; set; }
    public long? EmployeeId { get; set; }  
    public int UnitId { get; set; }
    public int ProductTypeId { get; set; }
    public int AreaId { get; set; }
}
