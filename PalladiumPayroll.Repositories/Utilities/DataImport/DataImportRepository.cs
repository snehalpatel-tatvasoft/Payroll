using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;

namespace PalladiumPayroll.Repositories.Utilities.DataImport;

public class DataImportRepository : IDataImportRepository
{
    private readonly DapperContext _dapper;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public DataImportRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _dapper = new DapperContext(configuration);
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TableDataModel<PayrollProcessingTransactionDto>> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", reqModel.CompanyId);
        parameters.Add("@CurrentPage", reqModel.CurrentPage);
        parameters.Add("@PageSize", reqModel.PageSize);
        parameters.Add("@SortBy", reqModel.SortBy ?? "PayrollProcessId");
        parameters.Add("@SortType", reqModel.SortType ? "ASC" : "DESC");
        parameters.Add("@Search", reqModel.Search ?? "");
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var data = await _dapper.ExecuteStoredProcedure<PayrollProcessingTransactionDto>("usp_GetPayrollProcessingTransactionsByCompany", parameters);
        var total = parameters.Get<int>("@TotalCount");

        return new TableDataModel<PayrollProcessingTransactionDto>
        {
            DataList = data,
            TotalCount = total
        };
    }
    public async Task<string?> EmployeeMasterfileImport(EmployeeMasterImportRequestDTO request)
    {
        foreach (var item in request.Data)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", request.CompanyId);
            parameters.Add("@EmployeeCode", item.EmployeeCode);
            parameters.Add("@EmployeeName", item.EmployeeName);
            parameters.Add("@EmployeeSurname", item.EmployeeSurname);
            parameters.Add("@Initials", item.Initials);
            parameters.Add("@Gender", item.Gender);
            parameters.Add("@PreferredName", item.PreferredName);
            parameters.Add("@IDNumber", item.IDNumber);
            parameters.Add("@DateOfBirth", item.DateOfBirth);
            parameters.Add("@PassportNumber", item.PassportNumber);
            parameters.Add("@PassportIssuedBy", item.PassportIssuedBy);
            parameters.Add("@IsAsylumSeeker", item.IsAsylumSeeker);
            parameters.Add("@AsylumPermitNumber", item.AsylumPermitNumber);
            parameters.Add("@IsRefugee", item.IsRefugee);
            parameters.Add("@UnitNumber", item.UnitNumber);
            parameters.Add("@Phy_CountryId", item.Phy_CountryId);
            parameters.Add("@ComplexName", item.ComplexName);
            parameters.Add("@StreetNumber", item.StreetNumber);
            parameters.Add("@StreetName", item.StreetName);
            parameters.Add("@District", item.District);
            parameters.Add("@City", item.City);
            parameters.Add("@Phy_PostalCode", item.Phy_PostalCode);
            parameters.Add("@IsPostalSame", item.IsPostalSame);
            parameters.Add("@Address1", item.Address1);
            parameters.Add("@Address2", item.Address2);
            parameters.Add("@Address3", item.Address3);
            parameters.Add("@Pos_PostalCode", item.Pos_PostalCode);
            parameters.Add("@HomeNumber", item.HomeNumber);
            parameters.Add("@CellNumber", item.CellNumber);
            parameters.Add("@Email", item.Email);
            parameters.Add("@StartDate", item.StartDate);
            parameters.Add("@IncomeTaxNumber", item.IncomeTaxNumber);
            parameters.Add("@NatureofPersonId", item.NatureofPersonId);
            parameters.Add("@DesignationId", item.DesignationId);
            parameters.Add("@PayrollSetupId", item.PayrollSetupId);
            parameters.Add("@flagEndEmployment", item.FlagEndEmployment);
            parameters.Add("@EndEmploymentDate", item.EndEmploymentDate);
            parameters.Add("@ProfileId", item.ProfileId);
            parameters.Add("@EmploymentStatus", item.EmploymentStatus);
            parameters.Add("@PaymentTypeId", item.PaymentTypeId);
            parameters.Add("@Bank1Id", item.Bank1Id);
            parameters.Add("@AccountTypeId", item.AccountTypeId);
            parameters.Add("@AccountName", item.AccountName);
            parameters.Add("@AccountNumber", item.AccountNumber);
            parameters.Add("@Branch1Code", item.Branch1Code);
            parameters.Add("@SplitAmount1", item.SplitAmount1);
            parameters.Add("@SplitPercentage1", item.SplitPercentage1);
            parameters.Add("@IsJointAccount", item.IsJointAccount);
            parameters.Add("@AccountHolderRelationship", item.AccountHolderRelationship);
            parameters.Add("@Branch2Code", item.Branch2Code);
            parameters.Add("@Bank2Id", item.Bank2Id);
            parameters.Add("@AccountName1", item.AccountName1);
            parameters.Add("@AccountNumber1", item.AccountNumber1);
            parameters.Add("@AccountTypeId1", item.AccountTypeId1);
            parameters.Add("@SplitAmount2", item.SplitAmount2);
            parameters.Add("@SplitPercentage2", item.SplitPercentage2);
            parameters.Add("@IsJointAccount1", item.IsJointAccount1);
            parameters.Add("@AccountHolderRelationship1", item.AccountHolderRelationship1);
            parameters.Add("@Name", item.Name);
            parameters.Add("@Relationship", item.Relationship);
            parameters.Add("@Phone", item.Phone);
            parameters.Add("@DepartmentId", item.DepartmentId);
            parameters.Add("@RaceId", item.RaceId);
            parameters.Add("@Title", item.Title);
            parameters.Add("@CopyCompanyAddress", item.CopyCompanyAddress);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

            var result = await _dapper.ExecuteStoredProcedureSingle<string>("usp_ImportEmployeesMasterFile", parameters);

            if (result == "DUPLICATE")
                return "Duplicate employee code";

            if (result == "ERROR")
                return "Error importing employee";
        }

        return null;
    }

    public async Task<bool> AddImportYearToDateTemplate(ImportYearToDateTemplateDto request)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@TemplateName", request.TemplateName);
        parameters.Add("@TransactionList", request.TransactionList);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_AddImportYearToDateTemplate", parameters);
        return parameters.Get<bool>("@IsSuccess");
    }

    public async Task<List<YTDTemplateDropdownDto>> GetDropDownForYearToDateTemplate(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<YTDTemplateDropdownDto>? result = await _dapper.ExecuteStoredProcedure<YTDTemplateDropdownDto>(
            "usp_GetDropDownForYearToDateTemplate",
            parameters
        );
        return result ?? new List<YTDTemplateDropdownDto>();
    }

    public async Task<List<TransactionForExcelGenerateDto>> GetTransactionForExcelGenerate(int templateId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@TemplateId", templateId);

        List<TransactionForExcelGenerateDto>? result = await _dapper.ExecuteStoredProcedure<TransactionForExcelGenerateDto>(
            "usp_GetTransactionDescriptionsByTemplateId",
            parameters
        );
        return result ?? new List<TransactionForExcelGenerateDto>();
    }

    public async Task<bool> ImportYTDRecord(YearToDateRecordDTO record)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeCode", record.EmployeeCode);
        parameters.Add("@Description", record.Description);
        parameters.Add("@Amount", record.Amount);
        parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_ImportYearToDateTransactions", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }

    private DataTable ConvertToDataTable(List<WorkInformationDTO> workInformations)
    {
        DataTable table = new DataTable();
        table.Columns.Add("EmployeeCode", typeof(string));
        table.Columns.Add("AnnualSalary", typeof(decimal));
        table.Columns.Add("MonthlySalary", typeof(decimal));
        table.Columns.Add("RatePerDay", typeof(decimal));
        table.Columns.Add("RatePerHour", typeof(decimal));
        table.Columns.Add("DaysPerWeek", typeof(decimal));
        table.Columns.Add("HoursPerWeek", typeof(decimal));
        table.Columns.Add("HoursPerDay", typeof(decimal));
        table.Columns.Add("StandardWorkingDays", typeof(string));

        foreach (var item in workInformations)
        {
            table.Rows.Add(
                item.EmployeeCode,
                item.AnnualSalary,
                item.MonthlySalary,
                item.RatePerDay,
                item.RatePerHour,
                item.DaysPerWeek,
                item.HoursPerWeek,
                item.HoursPerDay,
                item.StandardWorkingDays
            );
        }

        return table;
    }

    public async Task<string> ImportWorkInformation(WorkInformationImportRequestDTO importDto)
    {
        DataTable? table = ConvertToDataTable(importDto.Records);
        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("@WorkInfoTable", table.AsTableValuedParameter("dbo.WorkInformationTableType"));
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@TemplateName", importDto.TemplateName);
        parameters.Add("@ImportFileName", importDto.ImportFileName);
        parameters.Add("@CompanyId", importDto.CompanyId);

        var resultMessage = await _dapper.ExecuteStoredProcedureSingle<string>(
            "usp_ImportWorkInformation",
            parameters);

        return resultMessage ?? "No response from procedure";
    }


    public async Task<TableDataModel<ImportStatusDto>> GetImportStatus(ImportStatusFilterViewModel reqModel)
    {
        DynamicParameters parameters = new();
        parameters.Add("@CompanyId", reqModel.CompanyId);
        parameters.Add("@TemplateName", reqModel.TemplateName ?? string.Empty);
        parameters.Add("@CurrentPage", reqModel.CurrentPage);
        parameters.Add("@PageSize", reqModel.PageSize);
        parameters.Add("@SortBy", reqModel.SortBy ?? "CreatedDate");
        parameters.Add("@SortType", reqModel.SortType ? "ASC" : "DESC");
        parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

        List<ImportStatusDto>? data = await _dapper.ExecuteStoredProcedure<ImportStatusDto>("usp_GetImportStatus", parameters);
        int total = parameters.Get<int>("@TotalCount");

        return new TableDataModel<ImportStatusDto>
        {
            DataList = data,
            TotalCount = total
        };
    }


}
