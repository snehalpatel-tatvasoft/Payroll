namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company
{

    public class CompanyInfo
    {
        public Int64? CompanyId { get; set; }
        public string? CompanyLogo { get; set; }
        public string CompanyName { get; set; } = null!;
        public int CompanyTypeId { get; set; }
        public string CompanyRegNumber { get; set; } = null!;
        public long TaxRegNumber { get; set; }
        public int StdIndustryCode { get; set; }
        public string PAYEReferenceNumber { get; set; } = null!;
        public int TradeClassificationId { get; set; }
        public string UIFRefNumber { get; set; } = null!;
        public string UIFRegNumber { get; set; } = null!;
        public int? SplEcoZoneId { get; set; }
        public string SDLRefNumber { get; set; } = null!;
        public int CurrencyID { get; set; }
        public int CountryID { get; set; }
        public bool IsExemptSDL { get; set; }
        public bool UseBCEARemuneration { get; set; }
        public int EmployerDisentitlementId { get; set; }
        public string UnitNumber { get; set; }
        public string ComplexName { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public bool sameAddress { get; set; }
        public string? Pos_UnitNumber { get; set; }
        public string? Pos_ComplexName { get; set; }
        public string? Pos_StreetNumber { get; set; }
        public string? Pos_Street { get; set; }
        public string? Pos_District { get; set; }
        public string? Pos_City { get; set; }
        public string? Pos_PinCode { get; set; }
        public int? Pos_CountryId { get; set; }
        public string? Pos_Address1 { get; set; }
        public string? Pos_Address2 { get; set; }
        public string? Pos_Address3 { get; set; }
        public string? Pos_AddPinCode { get; set; }
    }

    public class CompanyRepresentative
    {
        public string SARSName { get; set; }
        public string SARSContactNo { get; set; }
        public string SARSContactEmail { get; set; }
    }

    public class CompanyPayrollCycle
    {
        public int CycleID { get; set; }
        public string CycleName { get; set; }
        public DateTime CycleEndDate { get; set; }
        public int CycleType { get; set; }
    }

    public class PayrollMedicalAidList
    {
        public int FundId { get; set; }
        public string FundName { get; set; }
        public string SchemeName { get; set; }
    }

    public class PayrollBenefitFundList
    {
        public int FundId { get; set; }
        public string FundName { get; set; }
        public int FundType { get; set; }
        public string ClearanceNo { get; set; }
        public int? PensionFund { get; set; }
        public int? ProvidentFund { get; set; }
        public decimal? CatFactor { get; set; }
        public decimal? RFIPercent { get; set; }
        public int FundCal { get; set; }
        public decimal? EmpCon { get; set; }
        public decimal? ComCon { get; set; }
    }

    public class CompanyBankAccount
    {
        public string? AccountHolderName { get; set; }
        public string? AccountNumber { get; set; }
        public int? TypeofAccount { get; set; }
        public int? BankId { get; set; }
        public int? BranchCode { get; set; }
    }

    public class CompanyModels
    {
        public CompanyInfo CompanyInfo { get; set; }
        public CompanyRepresentative CompanyRepresentative { get; set; }
        public int TaxYear { get; set; }
        public List<CompanyPayrollCycle>? PayrollCycles { get; set; }
        public List<PayrollMedicalAidList>? PayrollMedicalAidList { get; set; }
        public List<PayrollBenefitFundList>? PayrollBenefitFundList { get; set; }
        public CompanyBankAccount? CompanyBankAccount { get; set; }
    }


}
