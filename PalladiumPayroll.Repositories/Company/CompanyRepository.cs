using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using PalladiumPayroll.DTOs.Miscellaneous;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;
using Microsoft.AspNetCore.Http;
using System;

namespace PalladiumPayroll.Repositories.Company
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _dapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dapper = new DapperContext(configuration);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<long> CreateCompany(CreateCompanyRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CompanyName", request.Company);
            parameters.Add("@NoOfEmployee", request.NoOfEmployee);
            parameters.Add("@Country", request.Country);

            long companyId = await _dapper.ExecuteStoredProcedureSingle<long>("usp_CreateCompany", parameters);
            return companyId;
        }

        public async Task<Guid> CreateUser(CreateUserRequestDto request)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@UserName", request.FirstName);
            parameters.Add("@SurName", request.LastName);
            parameters.Add("@Email", request.Email);
            parameters.Add("@Password", request.Password);
            parameters.Add("@PasswordHash", request.PasswordHash);
            parameters.Add("@ContactNo", request.ContactNo);
            parameters.Add("@CompanyId", request.CompanyId);

            Guid userId = await _dapper.ExecuteStoredProcedureSingle<Guid>("usp_CreateUser", parameters);
            return userId;
        }

        public async Task<JsonResult> CompanyCreation(CompanyModels model)
        {
            var parameters = new DynamicParameters();

            // step-1 company info
            parameters.Add("@CompanyId", model.CompnayInfo.CompanyId);
            parameters.Add("@CompanyName", model.CompnayInfo.CompanyName);
            parameters.Add("@CompanyType", model.CompnayInfo.CompanyTypeId);
            parameters.Add("@CompanyRegNumber", model.CompnayInfo.CompanyRegNumber);
            parameters.Add("@TaxRegNumber", model.CompnayInfo.TaxRegNumber);
            parameters.Add("@StdIndustryCode", model.CompnayInfo.StdIndustryCode);
            parameters.Add("@PAYEReferenceNumber", model.CompnayInfo.PAYEReferenceNumber);
            parameters.Add("@TradeClassificationId", model.CompnayInfo.TradeClassificationId);
            parameters.Add("@UIFRegNumber", model.CompnayInfo.UIFRegNumber);
            parameters.Add("@SplEcoZoneId", model.CompnayInfo.SplEcoZoneId);
            parameters.Add("@UIFRefNumber", model.CompnayInfo.UIFRefNumber);
            parameters.Add("@CurrencyID", model.CompnayInfo.CurrencyID);
            parameters.Add("@SDLRefNumber", model.CompnayInfo.SDLRefNumber);
            parameters.Add("@CountryID", model.CompnayInfo.CountryID);
            parameters.Add("@IsExemptSDL", model.CompnayInfo.IsExemptSDL);
            parameters.Add("@UseBCEARemuneration", model.CompnayInfo.UseBCEARemuneration);

            parameters.Add("@EmployerDisentitlementId", model.CompnayInfo.EmployerDisentitlementId);
            //parameters.Add("@covid19", model.reli);

            parameters.Add("@UnitNumber", model.CompnayInfo.UnitNumber);
            parameters.Add("@ComplexName", model.CompnayInfo.ComplexName);
            parameters.Add("@StreetNumber", model.CompnayInfo.StreetNumber);
            parameters.Add("@Street", model.CompnayInfo.Street);
            parameters.Add("@District", model.CompnayInfo.District);
            parameters.Add("@City", model.CompnayInfo.City);
            parameters.Add("@PinCode", model.CompnayInfo.PinCode);
            parameters.Add("@IsPostalSame", model.CompnayInfo.IsPostalSame);
            parameters.Add("@Pos_Address1", model.CompnayInfo.Pos_Address1);
            parameters.Add("@Pos_Address2", model.CompnayInfo.Pos_Address2);
            parameters.Add("@Pos_Address3", model.CompnayInfo.Pos_Address3);
            parameters.Add("@Pos_PinCode", model.CompnayInfo.Pos_PinCode);
            parameters.Add("@Pos_CountryId", model.CompnayInfo.Pos_CountryId);

            // step-2 representive
            parameters.Add("@SARSName", model.CompanyRepresentive.SARSName);
            parameters.Add("@SARSContactEmail", model.CompanyRepresentive.SARSContactEmail);
            parameters.Add("@SARSContactNo", model.CompanyRepresentive.SARSContactNo);

            // step-3 payroll cycle setup
            DataTable payRollCycle = new DataTable();
            payRollCycle.Columns.Add("CycleID", typeof(int));
            payRollCycle.Columns.Add("CycleName", typeof(string));
            payRollCycle.Columns.Add("CycleTypeId", typeof(int));
            payRollCycle.Columns.Add("CycleEndDate", typeof(DateTime));
            foreach (var item in model.PayrollCycles)
            {
                DataRow row = payRollCycle.NewRow();
                row["CycleID"] = item.CycleID;
                row["CycleName"] = item.CycleName;
                row["CycleTypeId"] = item.CycleTypeId;
                row["CycleEndDate"] = item.CycleEndDate;
                payRollCycle.Rows.Add(row);
            }
            parameters.Add("@CycleRecord", payRollCycle.AsTableValuedParameter("dbo.CycleRecordType"));

            // step-4 general ledger


            // step-5 fund setup
            DataTable MedicalAid = new DataTable();
            MedicalAid.Columns.Add("MedAidId", typeof(int));
            MedicalAid.Columns.Add("MedAidFundName", typeof(string));
            MedicalAid.Columns.Add("MedAidSchemeType", typeof(int));
            foreach (var item in model.PayrollMedicalAidList)
            {
                DataRow row = MedicalAid.NewRow();
                row["MedAidId"] = item.MedAidId;
                row["MedAidFundName"] = item.MedAidFundName;
                row["MedAidSchemeType"] = item.MedAidSchemeType;
                MedicalAid.Rows.Add(row);
            }
            parameters.Add("@MedAidFundRecord", MedicalAid.AsTableValuedParameter("dbo.MedAidFundType"));

            DataTable BenifitFund = new DataTable();
            BenifitFund.Columns.Add("BenfId", typeof(int));
            BenifitFund.Columns.Add("ProvidentFundId", typeof(int));
            BenifitFund.Columns.Add("PensionFundId", typeof(int));
            BenifitFund.Columns.Add("BenfFundName", typeof(string));
            BenifitFund.Columns.Add("BenfFundType", typeof(string));
            BenifitFund.Columns.Add("ClearanceNo", typeof(string));
            BenifitFund.Columns.Add("Catfactor", typeof(decimal));
            BenifitFund.Columns.Add("Empcon", typeof(decimal));
            BenifitFund.Columns.Add("Comcon", typeof(decimal));
            BenifitFund.Columns.Add("RFIpercent", typeof(decimal));
            BenifitFund.Columns.Add("Fundcaltypeid", typeof(int));
            foreach (var item in model.PayrollBenefitFundLists)
            {
                DataRow row = BenifitFund.NewRow();
                row["BenfId"] = item.BenfId;
                row["ProvidentFundId"] = item.ProvidentFundType;
                row["PensionFundId"] = item.PensionFundType;
                row["BenfFundName"] = item.BenfFundName;
                row["BenfFundType"] = item.BenfFundType;
                row["ClearanceNo"] = item.ClearanceNo;
                row["Catfactor"] = item.Catfactor;
                row["Empcon"] = item.Empcon;
                row["Comcon"] = item.Comcon;
                row["RFIpercent"] = item.RFIpercent;
                row["Fundcaltypeid"] = item.Fundcaltypeid;
                BenifitFund.Rows.Add(row);
            }
            parameters.Add("@BenifitFundRecord", BenifitFund.AsTableValuedParameter("dbo.BenifitFundType"));

            // step-6 Bank Detail
            parameters.Add("@AccountHolderName", model.CompanyBankAccount.AccountHolderName);
            parameters.Add("@AccountNumber", model.CompanyBankAccount.AccountNumber);
            parameters.Add("@TypeofAccount", model.CompanyBankAccount.TypeofAccount);
            parameters.Add("@BankId", model.CompanyBankAccount.BankId);
            parameters.Add("@BranchCode", model.CompanyBankAccount.BranchCode);
            parameters.Add("@CreatedBy", 1);

            var isCreated = await _dapper.ExecuteStoredProcedureSingle<bool>("uusp_AddCompany", parameters);
            if (isCreated)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Company, ActionType.Created));
            }

            return HttpStatusCodeResponse.BadRequestResponse();
        }

        public async Task<bool> CheckCompanyExist(string company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyName", company);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CheckCompanyExists", parameters);
            return response;
        }

        public async Task<bool> AddNewBank(BankModel bankModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", bankModel.CompanyId);
            parameters.Add("@BankName", bankModel.BankName);
            parameters.Add("@BranchCode", bankModel.BranchCode);

            bool isAdded = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_AddBank", parameters);
            return isAdded;
        }
        
        public async Task<List<DropDownViewModel>> GetCompanyWithSubCompany(int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_GetCompanyWithChildren", parameters);
            return response;
        }

        public async Task<bool> SetActiveCompanyId(int companyId)
        {
            string userId = _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value;

            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            parameters.Add("@UserId", userId);

            bool isAdded = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_SetActiveCompanyId", parameters);
            return isAdded;
        }
    }
}
