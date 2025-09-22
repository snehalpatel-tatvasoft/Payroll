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
        public DateTime ProcessStartDate { get; set; }
        public DateTime ProcessEndDate { get; set; }
        public bool? IsProcessed { get; set; }
        public bool? IsSpecialRun { get; set; }
    }

    public class IndustryCouncil
    {
        public int CompanyId { get; set; }
        public bool ShiftOnTimeTakeonScreen { get; set; }
        public bool LimitShiftsToDaysPerCycle { get; set; }
        public bool PaySeifsa4thLeaveWeekSeparately { get; set; }
        public decimal TotalShiftPerYear { get; set; }
        public decimal BonusPercentage { get; set; }
        public int CouncilOptionId { get; set; }
        public FurnitureCouncil FurnitureCouncil { get; set; } = new FurnitureCouncil();
        public MIBFACouncil MIBFACouncil { get; set; } = new MIBFACouncil();
        public MIBFACouncilSetupDetail? MIBFACouncilSetupDetail { get; set; } = new MIBFACouncilSetupDetail();
    }

    public class FurnitureCouncil
    {
        public decimal HolidayHours1 { get; set; }
        public decimal HolidayHours2 { get; set; }
        public decimal HolidayHours3 { get; set; }
        public decimal Entitlement1 { get; set; }
        public decimal Entitlement2 { get; set; }
        public decimal Entitlement3 { get; set; }
        public decimal LoadSheddingHours { get; set; }
    }

    public class MIBFACouncil
    {
        public List<Int32>? SickFundReport { get; set; } = new List<Int32>();
        public List<Int32>? PensionFundReport { get; set; } = new List<Int32>();
        public List<Int32>? ProvidentFundReport { get; set; } = new List<Int32>();
        public List<Int32>? CouncilLevyReturnReport { get; set; } = new List<Int32>();
        public List<Int32>? MIBFAReport { get; set; } = new List<Int32>();
        public string FirmNumber { get; set; }
        public string TradeUnionCode { get; set; }
    }

    public class MIBFACouncilSetupDetail
    {
        public string SickFundTransactionsIds { get; set; }
        public string PensionFundTransactionsIds { get; set; }
        public string ProvidentFundTransactionsIds { get; set; }
        public string CouncilLevyReturnTransactionsIds { get; set; }
        public string MIBFATransactionsIds { get; set; }
        public string FirmNumber { get; set; }
        public string TradeUnionCode { get; set; }
    }

    public class MIBFAOptions
    {
        public List<MIBFASelectOptions> SickFundReportOptions { get; set; }
        public List<MIBFASelectOptions> PensionFundReportOptions { get; set; }
        public List<MIBFASelectOptions> ProvidentFundReportOptions { get; set; }
        public List<MIBFASelectOptions> CouncilLevyReturnReportOptions { get; set; }
        public List<MIBFASelectOptions> MIBFAReportOptions { get; set; }
    }

    public class MIBFASelectOptions
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class TransactionDropdown
    {
        public int PayrollProcessId { get; set; }
        public string Description { get; set; }
    }
}
