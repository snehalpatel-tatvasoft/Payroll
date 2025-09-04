namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;

public class PieceWorkDropdownItem
{
    public string Name { get; set; } = "";
    public long CompanyId { get; set; }
    public int Type { get; set; }
}

public class UpsertPieceworkMasterDataDTO
{
    public string ByUnitOrEmployee { get; set; } = string.Empty;
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

public class UpsertPieceworkDTO
{
    public int? PieceworkId { get; set; }
    public long EmployeeId { get; set; }
    public int AreaId { get; set; }
    public int ProductTypeId { get; set; }
    public int UnitId { get; set; }
    public decimal? QuantityDelivered { get; set; }
    public decimal? TotalPaidAmount { get; set; }
    public decimal? Rate { get; set; }
    public bool InfluenceUIF { get; set; }
    public bool InfluenceSDL { get; set; }
    public DateTime? PaymentDate { get; set; }
    public long CompanyId { get; set; }
}

public class PieceWorkFilterViewModel
{
    public long CompanyId { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "PieceworkId";
    public bool SortType { get; set; } = true;
    public string Search { get; set; } = "";
}