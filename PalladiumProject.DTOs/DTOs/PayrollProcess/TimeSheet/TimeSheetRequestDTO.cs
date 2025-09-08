using Microsoft.AspNetCore.Http;

namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.TimeSheet
{
    public class ImportTimeSheetRequest
    {
        public IFormFile File { get; set; } = null!;
        public bool IsHeader { get; set; } = true;
    }
}
