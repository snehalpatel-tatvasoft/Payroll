namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.HRFunctions.DisciplinaryLog
{
    public class DisciplinaryLogResponseDTO
    {
        public long DisciplinaryLogId { get; set; }
        public string EmployeeCode { get; set; }= "";
        public string EmployeeName { get; set; }= "";
        public DateTime ActionDate { get; set; }
        public string Representative { get; set; }= "";
        public string Charge { get; set; }= "";
        public string Witnesses { get; set; }= "";
        public string Result { get; set; }= "";
        public string FileName { get; set; }= "";
        public string FilePath { get; set; }= "";
    }

    public class DisciplinaryLogByIdResponseDTO
    {
        public long DisciplinaryLogId { get; set; }
        public long EmployeeId { get; set; }
        public long CompanyId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Representative { get; set; } = "";
        public string Charge { get; set; } = "";
        public string Witnesses { get; set; } = "";
        public string Result { get; set; } = "";
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public string? FileType { get; set; }
        public string EmployeeCode { get; set; } = "";
        public string EmployeeName { get; set; } = "";
    }

    public class EmployeeDropdownDTO
    {
        public long EmployeeId { get; set; }
        public string FullName { get; set; }= "";
    }
}