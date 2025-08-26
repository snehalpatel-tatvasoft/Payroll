using Microsoft.AspNetCore.Http;

namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.HRFunctions.DisciplinaryLog
{
    // public class DisciplinaryLogRequestDTO
    // {
    //     public long EmployeeId { get; set; }
    //     public long CompanyId { get; set; }
    //     public DateTime ActionDate { get; set; }
    //     public string Representative { get; set; }
    //     public string Charge { get; set; }
    //     public string Witnesses { get; set; }
    //     public string Result { get; set; }
    //     public string FileName { get; set; }
    //     public string FilePath { get; set; }
    //     public string FileType { get; set; }
    //     public long? FileSize { get; set; }
    //     public string CreatedBy { get; set; }

    // }

    // public class DisciplinaryLogEditRequestDTO
    // {
    //     public long DisciplinaryLogId { get; set; }
    //     public long EmployeeId { get; set; }
    //     public long CompanyId { get; set; }
    //     public DateTime ActionDate { get; set; }
    //     public string Representative { get; set; }
    //     public string Charge { get; set; }
    //     public string Witnesses { get; set; }
    //     public string Result { get; set; }
    //     public string FileName { get; set; }
    //     public string FilePath { get; set; }
    //     public string UpdatedBy { get; set; }
    // }

    public class DisciplinaryLogUpsertDTO
    {
        public long? DisciplinaryLogId { get; set; }
        public long EmployeeId { get; set; }
        public long CompanyId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Representative { get; set; }="";
        public string Charge { get; set; }="";
        public string Witnesses { get; set; }="";
        public string Result { get; set; }="";

        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public long? FileSize { get; set; }

        public IFormFile? File { get; set; }
    }

}
