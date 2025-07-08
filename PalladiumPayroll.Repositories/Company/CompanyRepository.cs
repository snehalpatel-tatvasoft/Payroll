using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using PalladiumPayroll.DTOs.Miscellaneous;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

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

        public async Task<List<DropDownViewModel>> GetGLAccounts(DBConnectionModel dbConnectionModel)
        {
            string connectionString =
                $"Data Source={dbConnectionModel.ServerName}; Initial Catalog={dbConnectionModel.DBName}; User ID={dbConnectionModel.UserName}; Password={dbConnectionModel.Password}; TrustServerCertificate=True;";

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = "SELECT Id, Name, Department FROM Employees"; // Adjust as needed
            var result = await connection.QueryAsync<DropDownViewModel>(sql);
            return result.ToList();
        }
        public async Task<long> CreateCompany(CreateCompanyRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CompanyName", request.Company);
            parameters.Add("@NoOfEmployee", request.NoOfEmployee);
            parameters.Add("@Country", request.Country);

            long companyId = await _dapper.ExecuteStoredProcedureSingle<long>("sp_CreateCompany", parameters);
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

            Guid userId = await _dapper.ExecuteStoredProcedureSingle<Guid>("sp_CreateUser", parameters);
            return userId;
        }

        public async Task<JsonResult> CompanyCreation(CompanyModels model)
        {
            var parameters = new DynamicParameters();

            // step-1 company info
            parameters.Add("@ActiveCompanyId", model.CompanyInfo.CompanyId);
            parameters.Add("@CompanyName", model.CompanyInfo.CompanyName);
            parameters.Add("@CompanyLogo", model.CompanyInfo.CompanyLogo);
            parameters.Add("@CompanyType", model.CompanyInfo.CompanyTypeId);
            parameters.Add("@CompanyRegNumber", model.CompanyInfo.CompanyRegNumber);
            parameters.Add("@TaxRegNumber", model.CompanyInfo.TaxRegNumber);
            parameters.Add("@StdIndustryCode", model.CompanyInfo.StdIndustryCode);
            parameters.Add("@PAYEReferenceNumber", model.CompanyInfo.PAYEReferenceNumber);
            parameters.Add("@TradeClassificationId", model.CompanyInfo.TradeClassificationId);
            parameters.Add("@UIFRegNumber", model.CompanyInfo.UIFRegNumber);
            parameters.Add("@SplEcoZoneId", model.CompanyInfo.SplEcoZoneId);
            parameters.Add("@UIFRefNumber", model.CompanyInfo.UIFRefNumber);
            parameters.Add("@CurrencyID", model.CompanyInfo.CurrencyID);
            parameters.Add("@SDLRefNumber", model.CompanyInfo.SDLRefNumber);
            parameters.Add("@CountryID", model.CompanyInfo.CountryID);
            parameters.Add("@IsExemptSDL", model.CompanyInfo.IsExemptSDL);
            parameters.Add("@UseBCEARemuneration", model.CompanyInfo.UseBCEARemuneration);
            parameters.Add("@EmployerDisentitlementId", model.CompanyInfo.EmployerDisentitlementId);
            parameters.Add("@UnitNumber", model.CompanyInfo.UnitNumber);
            parameters.Add("@ComplexName", model.CompanyInfo.ComplexName);
            parameters.Add("@StreetNumber", model.CompanyInfo.StreetNumber);
            parameters.Add("@StreetName", model.CompanyInfo.Street);
            parameters.Add("@District", model.CompanyInfo.District);
            parameters.Add("@City", model.CompanyInfo.City);
            parameters.Add("@PostalCode", model.CompanyInfo.PinCode);
            var isPostalSame = model.CompanyInfo.sameAddress;
            parameters.Add("@IsPostalSame", isPostalSame);
            parameters.Add("@Pos_UnitNumber", isPostalSame ? model.CompanyInfo.UnitNumber : null);
            parameters.Add("@Pos_ComplexName", isPostalSame ? model.CompanyInfo.ComplexName : null);
            parameters.Add("@Pos_StreetNumber", isPostalSame ? model.CompanyInfo.StreetNumber : null);
            parameters.Add("@Pos_StreetName", isPostalSame ? model.CompanyInfo.Street : null);
            parameters.Add("@Pos_District", isPostalSame ? model.CompanyInfo.District : null);
            parameters.Add("@Pos_City", isPostalSame ? model.CompanyInfo.City : null);
            parameters.Add("@Pos_PostalCode", isPostalSame ? model.CompanyInfo.PinCode : null);
            parameters.Add("@Pos_Address1", model.CompanyInfo.Pos_Address1);
            parameters.Add("@Pos_Address2", model.CompanyInfo.Pos_Address2);
            parameters.Add("@Pos_Address3", model.CompanyInfo.Pos_Address3);
            parameters.Add("@Pos_AddressPostalCode", model.CompanyInfo.Pos_PinCode);
            parameters.Add("@Pos_CountryId", model.CompanyInfo.Pos_CountryId);

            // step-2 representative
            parameters.Add("@SARSName", model.CompanyRepresentative.SARSName);
            parameters.Add("@SARSContactEmail", model.CompanyRepresentative.SARSContactEmail);
            parameters.Add("@SARSContactNo", model.CompanyRepresentative.SARSContactNo);

            // step-3 payroll cycle setup
            DataTable payRollCycle = new DataTable();
            payRollCycle.Columns.Add("CycleID", typeof(int));
            payRollCycle.Columns.Add("CycleName", typeof(string));
            payRollCycle.Columns.Add("CycleTypeId", typeof(int));
            payRollCycle.Columns.Add("CycleEndDate", typeof(DateTime));
            if (model.PayrollCycles != null)
            {
                foreach (var item in model.PayrollCycles)
                {
                    DataRow row = payRollCycle.NewRow();
                    row["CycleID"] = item.CycleID;
                    row["CycleName"] = item.CycleName;
                    row["CycleTypeId"] = item.CycleType;
                    row["CycleEndDate"] = item.CycleEndDate;
                    payRollCycle.Rows.Add(row);
                }
            }
            parameters.Add("@TaxYear", model.TaxYear);
            parameters.Add("@CycleRecord", payRollCycle.AsTableValuedParameter("dbo.CycleRecordType"));

            // step-4 general ledger
            DataTable glTransaction= new DataTable();
            glTransaction.Columns.Add("TransactionOrders", typeof(string));
            glTransaction.Columns.Add("DebitAccountNumber", typeof(string));
            glTransaction.Columns.Add("CreditAccountNumber", typeof(string));
            glTransaction.Columns.Add("ContraAccountNumber", typeof(string));

            if(model.TransactionList != null)
            {
                foreach (var item in model.TransactionList)
                {
                    DataRow row = glTransaction.NewRow();
                    row["TransactionOrders"] = item.TransactionOrders;
                    row["DebitAccountNumber"] = item.DebitAccountNumber;
                    row["CreditAccountNumber"] = item.CreditAccountNumber;
                    row["ContraAccountNumber"] = item.ContraAccountNumber;
                    glTransaction.Rows.Add(row);
                }
            }
            parameters.Add("@GLTransaction", glTransaction.AsTableValuedParameter("dbo.GLTransactionType"));

            // step-5 fund setup
            DataTable MedicalAid = new DataTable();
            MedicalAid.Columns.Add("MedAidId", typeof(int));
            MedicalAid.Columns.Add("MedAidFundName", typeof(string));
            MedicalAid.Columns.Add("MedAidSchemeName", typeof(string));
            if (model.PayrollMedicalAidList != null)
            {
                foreach (var item in model.PayrollMedicalAidList)
                {
                    DataRow row = MedicalAid.NewRow();
                    row["MedAidId"] = item.FundId;
                    row["MedAidFundName"] = item.FundName;
                    row["MedAidSchemeName"] = item.SchemeName;
                    MedicalAid.Rows.Add(row);
                }
            }
            parameters.Add("@MedAidFundRecord", MedicalAid.AsTableValuedParameter("dbo.MedAidFundType"));

            DataTable BenifitFund = new DataTable();
            BenifitFund.Columns.Add("BenId", typeof(int));
            BenifitFund.Columns.Add("BenFundName", typeof(string));
            BenifitFund.Columns.Add("BenFundType", typeof(string));
            BenifitFund.Columns.Add("ProvidentFund", typeof(int));
            BenifitFund.Columns.Add("PensionFund", typeof(int));
            BenifitFund.Columns.Add("ClearanceNo", typeof(string));
            BenifitFund.Columns.Add("RFIPercent", typeof(decimal));
            BenifitFund.Columns.Add("CatFactor", typeof(int));
            BenifitFund.Columns.Add("FundCalType", typeof(int));
            BenifitFund.Columns.Add("EmpCon", typeof(decimal));
            BenifitFund.Columns.Add("ComCon", typeof(decimal));
            if (model.PayrollBenefitFundList != null)
            {
                foreach (var item in model.PayrollBenefitFundList)
                {
                    DataRow row = BenifitFund.NewRow();
                    row["BenId"] = item.FundId;
                    row["BenFundName"] = item.FundName;
                    row["BenFundType"] = item.FundType;
                    row["ProvidentFund"] = item.ProvidentFund ?? (object)DBNull.Value;
                    row["PensionFund"] = item.PensionFund ?? (object)DBNull.Value;
                    row["ClearanceNo"] = item.ClearanceNo;
                    row["RFIPercent"] = item.RFIPercent;
                    row["CatFactor"] = item.CatFactor;
                    row["FundCalType"] = item.FundCal;
                    row["EmpCon"] = item.EmpCon;
                    row["ComCon"] = item.ComCon;
                    BenifitFund.Rows.Add(row);
                }
            }
            parameters.Add("@BenifitFundRecord", BenifitFund.AsTableValuedParameter("dbo.BenifitFundType"));

            // step-6 Bank Detail
            parameters.Add("@AccountHolderName", model.CompanyBankAccount?.AccountHolderName);
            parameters.Add("@AccountNumber", model.CompanyBankAccount?.AccountNumber);
            parameters.Add("@TypeofAccount", model.CompanyBankAccount?.TypeofAccount);
            parameters.Add("@BankId", model.CompanyBankAccount?.BankId);
            parameters.Add("@BranchCode", model.CompanyBankAccount?.BranchCode);
            parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

            var isCreated = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_AddCompany", parameters);
            if (isCreated)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Company, ActionType.Created));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
        }

        public async Task<bool> CheckCompanyExist(string company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyName", company);

            bool response = await _dapper.ExecuteStoredProcedureSingle<bool>("sp_CheckCompanyExists", parameters);
            return response;
        }

        public async Task<bool> CheckCompanyExist(int companyId, string companyName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            parameters.Add("@CompanyName", companyName);

            return await _dapper.ExecuteStoredProcedureSingle<bool>("sp_CheckSubCompanyExists", parameters);
        }

        public async Task<bool> AddNewBank(BankModel bankModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", bankModel.CompanyId);
            parameters.Add("@BankName", bankModel.BankName);
            parameters.Add("@BranchCode", bankModel.BranchCode);

            bool isAdded = await _dapper.ExecuteStoredProcedureSingle<bool>("sp_AddBank", parameters);
            return isAdded;
        }
        
        public async Task<List<DropDownViewModel>> GetCompanyWithSubCompany(int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("sp_GetCompanyWithChildren", parameters);
            return response;
        }

        public async Task<bool> SetActiveCompanyId(int companyId)
        {
            string userId = _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value;

            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            parameters.Add("@UserId", userId);

            bool isAdded = await _dapper.ExecuteStoredProcedureSingle<bool>("sp_SetActiveCompanyId", parameters);
            return isAdded;
        }
    }
}
