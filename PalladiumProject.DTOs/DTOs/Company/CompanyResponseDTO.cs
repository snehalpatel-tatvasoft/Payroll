using PalladiumPayroll.DTOs.DTOs.Common;

namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Company
{
    public class GLConnRes
    {
        public bool IsDbConnection { get; set; }
        public string ConnectionMessage { get; set; }
        public List<DropDownViewModelWithString> GlAccountList { get; set; }
        public List<DropDownViewModelWithString> GlDepartmentList { get; set; }
    }

    public class CyelePeriod
    {
        public int ProcessingCyclePeriodId { get; set; }
        public string CycleName { get; set; }
        public int CycleType { get; set; }
        public DateTime ProcessStartDate { get; set; }
        public DateTime ProcessEndDate { get; set; }
        public bool? IsProcessed { get; set; }
        public bool? IsSpecialRun { get; set; }
    }
}
