

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company
{

    public class CompanyType
    {
        public int CompanyTypeId { get; set; }
        public string CompanyTypeName { get; set; }
    }

    public class DisEntitle
    {
        public int EmployerDisentitlementId { get; set; }
        public string EmployerDisentitlement { get; set; }
    }

    public class TaxYears
    {
        public int TaxYearId { get; set; }
        public string TaxYear { get; set; }
    }

    public class PensionFundType
    {
        public int PensionFundId { get; set; }
        public string PensionFund { get; set; }
    }

    public class EssTransactionList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SpecialEconomicZone
    {
        public int ZoneId { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
    }

    public class Countries
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }

    public class ProvidentFundType
    {
        public int ProvidentFundId { get; set; }
        public string ProvidentFund { get; set; }
    }

    public class Currency
    {
        public int ID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
    }
    public class TaxMethod
    {
        public int ID { get; set; }
        public string TaxMethodType { get; set; }

    }


    public class CompanyModels1
    {
        public int SectionID { get; set; }
        public bool IsSuperAdmin { get; set; }

        public string strAddNewCompany { get; set; }

        public int CompanyId { get; set; }

        [Required]
        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public string CompanyName { get; set; }
        public string CompanyRegNumber { get; set; }
        public long? TaxRegNumber { get; set; }
        public string PAYEReferenceNumber { get; set; }

        public string UIFRefNumber { get; set; }
        public string UIFRegNumber { get; set; }
        public string SDLRefNumber { get; set; }

        public string UnitNumber { get; set; }
        public string ComplexName { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; } 
        public bool CheckSameAddress { get; set; }
        public string Pos_UnitNumber { get; set; }
        public string Pos_ComplexName { get; set; }
        public string Pos_StreetNumber { get; set; }
        public string Pos_Street { get; set; }
        public string Pos_District { get; set; }
        public string Pos_City { get; set; }
        public string Pos_PinCode { get; set; }
        public string Pos_Address1 { get; set; }
        public string Pos_Address2 { get; set; }
        public string Pos_Address3 { get; set; }
        public string Pos_AddPinCode { get; set; }

        public string StdIndustryCode { get; set; }

        public string PhysicalCountry { get; set; }
        public string PostalCountry { get; set; }
        public int? SplEcoZoneId { get; set; }
        public int? CurrencyID { get; set; }
        public int? CountryID { get; set; }
        public int? TaxMethod { get; set; }

        public Boolean IsExemptSDL { get; set; }
        public Boolean UseBCEARemuneration { get; set; }

        public string UIFName { get; set; }
        public string BatchId { get; set; }
        public string UIFContactNo { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Invalid Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UIFContactEmail { get; set; }

        public string SARSName { get; set; }
        public string SARSContactNo { get; set; }
        public string SARSContactEmail { get; set; }
        public decimal AnnualOIDLimit { get; set; }
        public decimal AnnualUIFLimit { get; set; }


        public string CycleName { get; set; }
        private DateTime cycleEndDate { get; set; } = DateTime.Now;
        public int CycletypeId { get; set; }

        public string AidFundName { get; set; }
        public string AidSchemeName { get; set; }
        public string FundName { get; set; }
        public string ClearanceNo { get; set; }

        [DataType(DataType.Currency)]
        public double? EmpContribution { get; set; }

        [DataType(DataType.Currency)]
        public double? ComContribution { get; set; }

        public int? CategoryFactor { get; set; }

        public int? FundCalculationType { get; set; }

        //FundCalculation

        //Dropdown related Id
        public int? TradeClassificationId { get; set; }
        public int? TradeClassificationGroupId { get; set; }
        public int? EmployerDisentitlementId { get; set; }
        public int? TaxYearId { get; set; }
        public int? PayrollCycleId { get; set; }
        public int? FundTypeId { get; set; }
        public string TradeClassificationCode { get; set; }
        public int? ProvidentFundId { get; set; }
        public int? PensionFundId { get; set; }
        public int? cycle_Id { get; set; }

        public int? Pos_CountryId { get; set; }
        public int? Phy_CountryId { get; set; }

        public string IsCompanyQualifyForTurnOver { get; set; }
        public string IsCompanyRegisterbeforeMarch { get; set; }

        //Dropdown related Id
        public bool Ispayrollcycle { get; set; }
        public bool IsEmployeeRenistate { get; set; }

        //Processing Periods
        public List<clsProcessPeriod> ListProcessPeriod { get; set; }
        public List<clsProcessPeriod> ListUpcomingProcessPeriod { get; set; }
        //Processing Periods End

        //Dropdown List
        public List<TradeClass> ListTradeClass { get; set; }

        public List<StdIndustryClass> ListStdIndustryCode { get; set; }

        public List<TradeClass> ListTradeGroup { get; set; }
        //public GLsettingModel GLsetting { get; set; }

        public List<DisEntitle> ListDisEntitle = new List<DisEntitle>
          {
           new DisEntitle(){ EmployerDisentitlement = "Disqualified", EmployerDisentitlementId=1 },
           new DisEntitle(){ EmployerDisentitlement = "Sphere of Government", EmployerDisentitlementId=2},
           new DisEntitle(){ EmployerDisentitlement = "Municipality", EmployerDisentitlementId=3},
           new DisEntitle(){ EmployerDisentitlement = "Not Registered for Employees Tax", EmployerDisentitlementId=4 },
           new DisEntitle(){ EmployerDisentitlement = "Public Entity", EmployerDisentitlementId=5},
           new DisEntitle(){ EmployerDisentitlement = "Entitled to ETI", EmployerDisentitlementId=6},
          };

        public List<PensionFundType> ListPensionfund = new List<PensionFundType>
        {
            new PensionFundType(){ PensionFund = "Defined Contribution Fund" ,PensionFundId = 1},
            new PensionFundType(){ PensionFund = "Defined Benefit Fund" ,PensionFundId = 2},
            new PensionFundType(){ PensionFund = "Hybrid Fund" ,PensionFundId = 3},
        };

        public List<ProvidentFundType> ListProvidentFund = new List<ProvidentFundType>
        {
            new ProvidentFundType(){ ProvidentFund = "Defined Contribution Fund" ,ProvidentFundId = 1},
            new ProvidentFundType(){ ProvidentFund = "Defined Benefit Fund" ,ProvidentFundId = 2},
            new ProvidentFundType(){ ProvidentFund = "Hybrid Fund" ,ProvidentFundId = 3},
        };

        public List<TaxYears> ListTaxYear { get; set; }

        //public List<MasterModels.MasterList> ListPayrollCycleType { get; set; }

        //public List<MasterModels.MasterList> ListFundType { get; set; }
        //public List<MasterModels.MasterList> ListTempFundType { get; set; }

        public List<Countries> ListCountriesClass { get; set; }

        public List<SpecialEconomicZone> ListSpecialEconomicZonesClass { get; set; }
        public List<Currency> ListCurrencyType { get; set; }
        public List<TaxMethod> ListTaxMethod { get; set; }
        public List<EssTransactionList> EssTransactionsList { get; set; } = new List<EssTransactionList>();


        //Bank Details property
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public int TypeofAccount { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string OtherBankName1 { get; set; }
        public string BranchCode1 { get; set; }
        public string OtherBranchCode1 { get; set; }
        public string ddlBranchCode1 { get; set; }
        public int AccountTypeId { get; set; }
        public int AccountTypeId1 { get; set; }
        public int? Bank1Id { get; set; }
        public List<SelectListAccountTypeItem> AccountType { get; set; }
        public List<SelectListBanksItem> BanksList { get; set; }
        public class SelectListAccountTypeItem
        {
            public int AccountTypeId { get; set; }
            public string AccountTypeName { get; set; }
        }
        public class SelectListBanksItem
        {
            public int BankId { get; set; }
            public string BankName { get; set; }
            public string BranchCode { get; set; }
        }
        //End Bank Details
        //Dropdown List


        public string CycleTypeName { get; set; }

        public class clsProcessPeriod
        {
            public int? periodId { get; set; }
            public string paycycleName { get; set; }
            public string periodName { get; set; }
            public string periodStartdate { get; set; }
            public string periodEnddate { get; set; }
            public string perioddate { get; set; }
            public string cycletypeName { get; set; }
            public int? cycletypeId { get; set; }
            public bool IsProcessed { get; set; }
            public bool IsSpecialRun { get; set; }
            public bool IsPayslipGenerated { get; set; }
            public string Period { get; set; }
        }

        public class PayrollCycleSetupList
        {
            public int? cycleId { get; set; }
            public int? payrollSetupid { get; set; }
            public string payrollCycleName { get; set; }
            public string payrollCycleType { get; set; }
            public string payrollCycleEndDate { get; set; }
            public string payrollCycleTypeId { get; set; }

            public bool isCalculateShift { get; set; } = false;

            public string EssTransactionListString { get; set; }

            //public int? taxyearid { get; set; }
        }

        public List<PayrollCycleSetupList> ListPayrollCycleSetup { get; set; }

        public List<PayrollMedicalAidList> ListPayrollMedicalAidSetup { get; set; }

        public class PayrollMedicalAidList
        {
            public int? MedAidId { get; set; }
            public int? MedAidSetupId { get; set; }
            public string payrollMedAidFundName { get; set; }
            public string payrollMedAidSchemeType { get; set; }
            public string BatchId { get; set; }
        }

        public List<PayrollBenefitFundList> ListPayrollBenefitFundSetup { get; set; }

        public class PayrollBenefitFundList
        {
            public int? BenfId { get; set; }
            public int? BenfSetupId { get; set; }
            public string payrollBenfFundName { get; set; }
            public string payrollBenfFundType { get; set; }

            public string payrollClearanceNo { get; set; }
            public string payrollPensionFundType { get; set; }

            public string payrollProvidentFundType { get; set; }

            public string fundcal { get; set; }
            public int fundcaltypeid { get; set; }
            public decimal? catfactor { get; set; }
            public decimal? empcon { get; set; }
            public decimal? comcon { get; set; }
            public decimal? RFIpercent { get; set; }
            public string BatchId { get; set; }
        }

        public List<CompanyType> ListCompanyType = new List<CompanyType>
        {
            new CompanyType(){ CompanyTypeName = "Sole Proprietor" ,CompanyTypeId = 1},
            new CompanyType(){ CompanyTypeName = "Private Company – (PTY) Ltd" ,CompanyTypeId = 2},
            new CompanyType(){ CompanyTypeName = "Personal Liability Company – Plc" ,CompanyTypeId = 3},
            new CompanyType(){ CompanyTypeName = "Public Companies (Ltd.)" ,CompanyTypeId = 4},
            new CompanyType(){ CompanyTypeName = "State Owned Companies – SOC" ,CompanyTypeId = 5},
            new CompanyType(){ CompanyTypeName = "Close Corporation" ,CompanyTypeId = 6},
        };

        public int? CompanyTypeId { get; set; }

        public string CompanyLogoUrl { get; set; }
    }


    public class TradeClass
	{
		public int TradeClassificationId { get; set; }
		public string TradeClassification { get; set; }
	}
	
	public class StdIndustryClass
	{
		public string StdIndustryCode { get; set; }
		public string StdIndustryCodeName { get; set; }
	}




    public class CompnayInfo
    {
        public Int64 CompanyId { get; set; }
        public IFormFile CompanyLogo { get; set; }
        public string CompanyName { get; set; }
        public int? CompanyTypeId { get; set; }
        public string CompanyRegNumber { get; set; }
        public long? TaxRegNumber { get; set; }
        public string StdIndustryCode { get; set; }
        public string PAYEReferenceNumber { get; set; }
        public int? TradeClassificationId { get; set; }
        public string UIFRefNumber { get; set; }
        public string UIFRegNumber { get; set; }
        public int? SplEcoZoneId { get; set; }
        public string SDLRefNumber { get; set; }
        public int? CurrencyID { get; set; }
        public int? CountryID { get; set; }
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
        public bool IsPostalSame { get; set; }
        public string Pos_UnitNumber { get; set; }
        public string Pos_ComplexName { get; set; }
        public string Pos_StreetNumber { get; set; }
        public string Pos_Street { get; set; }
        public string Pos_District { get; set; }
        public string Pos_City { get; set; }
        public string Pos_PinCode { get; set; }
        public int Pos_CountryId { get; set; }
        public string Pos_Address1 { get; set; }
        public string Pos_Address2 { get; set; }
        public string Pos_Address3 { get; set; }
        public string Pos_AddPinCode { get; set; }
    }

    public class CompanyRepresentive
    {
        public string SARSName { get; set; }
        public string SARSContactNo { get; set; }
        public string SARSContactEmail { get; set; }
    }

    public class CompanyPayrollCycle
    {
        public int? CycleID {  get; set; }
        public string CycleName { get; set; }
        public DateTime CycleEndDate { get; set; } = DateTime.Now;
        public int CycleTypeId { get; set; }
    }

    public class PayrollMedicalAidList
    {
        public int? MedAidId { get; set; }
        public int? MedAidSetupId { get; set; }
        public string MedAidFundName { get; set; }
        public int MedAidSchemeType { get; set; }
        public string BatchId { get; set; }
    }

    public class PayrollBenefitFundList
    {
        public int? BenfId { get; set; }
        public int? BenfSetupId { get; set; }
        public string BenfFundName { get; set; }
        public string BenfFundType { get; set; }
        public string ClearanceNo { get; set; }
        public int PensionFundType { get; set; }
        public int ProvidentFundType { get; set; }
        public string fundcal { get; set; }
        public int Fundcaltypeid { get; set; }
        public decimal? Catfactor { get; set; }
        public decimal? Empcon { get; set; }
        public decimal? Comcon { get; set; }
        public decimal? RFIpercent { get; set; }
        public string BatchId { get; set; }
    }

    public class CompanyBankAccount
    {
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public int TypeofAccount { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string BranchCode { get; set; }
    }

    public class TransactionList
    {
        public int TransactionOrders { get; set; }
        public int AllowanceTypeId { get; set; }
        public string? Description { get; set; }
        public string? DebitAccountNumber { get; set; }
        public string? CreditAccountNumber { get; set; }
        public string? ContraAccountNumber { get; set; }
    }

    public class CompanyModels
    {
        public CompnayInfo CompnayInfo { get; set; }
        public CompanyRepresentive CompanyRepresentive { get; set; }
        public List<CompanyPayrollCycle> PayrollCycles { get; set; }
        public List<PayrollMedicalAidList> PayrollMedicalAidList { get; set; }
        public List<PayrollBenefitFundList> PayrollBenefitFundLists { get; set; }
        public CompanyBankAccount CompanyBankAccount { get; set; }
        public List<TransactionList> TransactionList { get; set; }
        public int TaxYear { get; set; }
    }
    

}
