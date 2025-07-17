using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly DapperContext _dapper;
        public CommonRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

        public async Task<List<DropDownViewModel>> GetCountryList()
        {
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_FetchCountry");
            return response;
        }

        public async Task<List<DropDownViewModel>> GetTaxYearList()
        {
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_FetchTaxYear");
            return response;
        }

        public async Task<List<DropDownViewModel>> GetBankList(int? companyId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_FetchBank", parameters);
            return response;
        }

        public async Task<List<DropDownViewModel>> GetBranchList(int bankId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BankId", bankId);
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_FetchBranchCode", parameters);
            return response;
        }

        public async Task<List<DropDownViewModel>> GetStandardIndustryCode()
        {
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_FetchStandardIndustry");
            return response;
        }

        public async Task<List<DropDownViewModel>> GetTradeClassification()
        {
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_FetchTradeClassification");
            return response;
        }
        public async Task<List<TransactionList>> GetTransactionList()
        {
            List<TransactionList> response = await _dapper.ExecuteStoredProcedure<TransactionList>("usp_GetTransactionList");
            return response;
        }

        public async Task<bool> CheckDBConnection(DBConnectionModel dbConnectionModel)
        {
            string originalString = string.Format(DefaultConnectionString, dbConnectionModel.ServerName, dbConnectionModel.DBName, dbConnectionModel.UserName, dbConnectionModel.Password);
            return await DapperContext.CheckDBConnection(originalString);
        }

        public async Task<List<DropDownViewModel>> GetBusinessTypeList()
        {
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_GetBusinessTypeList");
            return response;
        }

        public async Task<List<DropDownViewModel>> GetNumberOfEmployeesList()
        {
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_GetNumberOfEmployeesList");
            return response;
        }

        public async Task<List<DropDownViewModel>> GetIndustryOrSectorTypeList()
        {
            List<DropDownViewModel> response = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_GetIndustryOrSectorTypeList");
            return response;
        }

        public async Task<bool> AddIndustrySectorType(string industrySector)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@IndustrySectorName", industrySector);

            bool isAdded = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_AddIndustrySectorType", parameters);
            return isAdded;
        }

        public async Task<bool> DeleteIndustrySector(int industrySectorId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IndustrySectorId", industrySectorId);

            bool isDeleted = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteIndustrySector", parameters);
            return isDeleted;
        }
    }
}
