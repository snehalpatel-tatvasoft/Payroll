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