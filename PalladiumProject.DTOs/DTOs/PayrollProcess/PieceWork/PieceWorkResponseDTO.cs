namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;

public class DeleteDropDownResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class UnitDto
{
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
}

public class ProductTypeDto
{
    public int ProductTypeId { get; set; }
    public string ProductTypeName { get; set; } = string.Empty;
}

public class AreaDto
{
    public int AreaId { get; set; }
    public string AreaName { get; set; } = string.Empty;
}

public class EmployeeDto
{
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeeSurname { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
}

public class PieceWorkDropdownsDTO
{
    public List<UnitDto> Units { get; set; } = new();
    public List<ProductTypeDto> ProductTypes { get; set; } = new();
    public List<AreaDto> Areas { get; set; } = new();
    public List<EmployeeDto> Employees { get; set; } = new();
}

public class PieceworkListDTO
{
    public int PieceworkId { get; set; }
    public long EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public int AreaId { get; set; }
    public string Area { get; set; } = string.Empty;
    public int ProductTypeId { get; set; }
    public string ProductType { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal? QuantityDelivered { get; set; }
    public decimal? Rate { get; set; }
    public decimal? TotalPaidAmount { get; set; }
    public bool InfluenceUIF { get; set; }
    public bool InfluenceSDL { get; set; }
    public DateTime? PaymentDate { get; set; }
    public long CompanyId { get; set; }
}