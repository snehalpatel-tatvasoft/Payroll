using System.Data;
using System.Text;
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

        string? userId = _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value;

        var table = EmployeeMasterToDataTable(request.Data);

        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@TemplateName", request.TemplateName);
        parameters.Add("@ImportFileName", request.ImportFileName);
        parameters.Add("@UserId", userId);
        parameters.Add("@Employees", table.AsTableValuedParameter("EmployeeMasterImportType"));

        var result = await _dapper.ExecuteStoredProcedureSingle<string>(
            "usp_ImportEmployeesMasterFile",
            parameters
        );

        return result == "SUCCESS" ? null : result;

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

    private DataTable YearToDateRecordsToDataTable(List<YearToDateRecordDTO> records)
    {
        DataTable table = new DataTable();
        table.Columns.Add("EmployeeCode", typeof(string));
        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("Amount", typeof(decimal));

        foreach (var record in records)
        {
            table.Rows.Add(record.EmployeeCode, record.Description, record.Amount);
        }

        return table;
    }

    public async Task<string> ImportYTDRecords(ImportYearToDateRecordRequestDTO importDto)
    {
        DataTable? dataTable = YearToDateRecordsToDataTable(importDto.Records);
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@YearToDateTable", dataTable.AsTableValuedParameter("dbo.YearToDateTableType"));
        parameters.Add("@TemplateName", importDto.TemplateName);
        parameters.Add("@ImportFileName", importDto.ImportFileName);
        parameters.Add("@CompanyId", importDto.CompanyId);
        parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

        var resultMessage = await _dapper.ExecuteStoredProcedureSingle<string>("usp_ImportYearToDateTransactions", parameters);

        return resultMessage ?? "No response from procedure";
    }


    private DataTable WorkInformationToDataTable(List<WorkInformationDTO> workInformations)
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
        DataTable? table = WorkInformationToDataTable(importDto.Records);
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

    private DataTable EmployeeMasterToDataTable(IEnumerable<EmployeeMasterImport> data)
    {
        var table = new DataTable();
        table.Columns.Add("EmployeeCode", typeof(string));
        table.Columns.Add("EmployeeName", typeof(string));
        table.Columns.Add("EmployeeSurname", typeof(string));
        table.Columns.Add("Initials", typeof(string));
        table.Columns.Add("Gender", typeof(int));
        table.Columns.Add("PreferredName", typeof(string));
        table.Columns.Add("IDNumber", typeof(string));
        table.Columns.Add("DateOfBirth", typeof(DateTime));
        table.Columns.Add("PassportNumber", typeof(string));
        table.Columns.Add("PassportIssuedBy", typeof(int));
        table.Columns.Add("IsAsylumSeeker", typeof(bool));
        table.Columns.Add("AsylumPermitNumber", typeof(string));
        table.Columns.Add("IsRefugee", typeof(bool));
        table.Columns.Add("UnitNumber", typeof(string));
        table.Columns.Add("Phy_CountryId", typeof(string));
        table.Columns.Add("ComplexName", typeof(string));
        table.Columns.Add("StreetNumber", typeof(string));
        table.Columns.Add("StreetName", typeof(string));
        table.Columns.Add("District", typeof(string));
        table.Columns.Add("City", typeof(string));
        table.Columns.Add("Phy_PostalCode", typeof(string));
        table.Columns.Add("IsPostalSame", typeof(bool));
        table.Columns.Add("Address1", typeof(string));
        table.Columns.Add("Address2", typeof(string));
        table.Columns.Add("Address3", typeof(string));
        table.Columns.Add("Pos_PostalCode", typeof(string));
        table.Columns.Add("HomeNumber", typeof(string));
        table.Columns.Add("CellNumber", typeof(string));
        table.Columns.Add("Email", typeof(string));
        table.Columns.Add("StartDate", typeof(DateTime));
        table.Columns.Add("IncomeTaxNumber", typeof(string));
        table.Columns.Add("NatureofPersonId", typeof(long));
        table.Columns.Add("DesignationId", typeof(string));
        table.Columns.Add("PayrollSetupId", typeof(string));
        table.Columns.Add("flagEndEmployment", typeof(bool));
        table.Columns.Add("EndEmploymentDate", typeof(DateTime));
        table.Columns.Add("ProfileId", typeof(string));
        table.Columns.Add("EmploymentStatus", typeof(int));
        table.Columns.Add("PaymentTypeId", typeof(string));
        table.Columns.Add("Bank1Id", typeof(string));
        table.Columns.Add("AccountTypeId", typeof(string));
        table.Columns.Add("AccountName", typeof(string));
        table.Columns.Add("AccountNumber", typeof(string));
        table.Columns.Add("Branch1Code", typeof(string));
        table.Columns.Add("SplitAmount1", typeof(decimal));
        table.Columns.Add("SplitPercentage1", typeof(decimal));
        table.Columns.Add("IsJointAccount", typeof(bool));
        table.Columns.Add("AccountHolderRelationship", typeof(string));
        table.Columns.Add("Branch2Code", typeof(string));
        table.Columns.Add("Bank2Id", typeof(string));
        table.Columns.Add("AccountName1", typeof(string));
        table.Columns.Add("AccountNumber1", typeof(string));
        table.Columns.Add("AccountTypeId1", typeof(string));
        table.Columns.Add("SplitAmount2", typeof(decimal));
        table.Columns.Add("SplitPercentage2", typeof(decimal));
        table.Columns.Add("IsJointAccount1", typeof(bool));
        table.Columns.Add("AccountHolderRelationship1", typeof(string));
        table.Columns.Add("[Name]", typeof(string));
        table.Columns.Add("Relationship", typeof(string));
        table.Columns.Add("Phone", typeof(string));
        table.Columns.Add("DepartmentId", typeof(string));
        table.Columns.Add("RaceId", typeof(string));
        table.Columns.Add("Title", typeof(int));
        table.Columns.Add("CopyCompanyAddress", typeof(bool));

        foreach (var item in data)
        {
            var row = table.NewRow();
            row["EmployeeCode"] = (object?)item.EmployeeCode ?? DBNull.Value;
            row["EmployeeName"] = (object?)item.EmployeeName ?? DBNull.Value;
            row["EmployeeSurname"] = (object?)item.EmployeeSurname ?? DBNull.Value;
            row["Initials"] = (object?)item.Initials ?? DBNull.Value;
            row["Gender"] = (object?)item.Gender ?? DBNull.Value;
            row["PreferredName"] = (object?)item.PreferredName ?? DBNull.Value;
            row["IDNumber"] = (object?)item.IDNumber ?? DBNull.Value;
            row["DateOfBirth"] = (object?)item.DateOfBirth ?? DBNull.Value;
            row["PassportNumber"] = (object?)item.PassportNumber ?? DBNull.Value;
            row["PassportIssuedBy"] = (object?)item.PassportIssuedBy ?? DBNull.Value;
            row["IsAsylumSeeker"] = (object?)item.IsAsylumSeeker ?? DBNull.Value;
            row["AsylumPermitNumber"] = (object?)item.AsylumPermitNumber ?? DBNull.Value;
            row["IsRefugee"] = (object?)item.IsRefugee ?? DBNull.Value;
            row["UnitNumber"] = (object?)item.UnitNumber ?? DBNull.Value;
            row["Phy_CountryId"] = (object?)item.Phy_CountryId ?? DBNull.Value;
            row["ComplexName"] = (object?)item.ComplexName ?? DBNull.Value;
            row["StreetNumber"] = (object?)item.StreetNumber ?? DBNull.Value;
            row["StreetName"] = (object?)item.StreetName ?? DBNull.Value;
            row["District"] = (object?)item.District ?? DBNull.Value;
            row["City"] = (object?)item.City ?? DBNull.Value;
            row["Phy_PostalCode"] = (object?)item.Phy_PostalCode ?? DBNull.Value;
            row["IsPostalSame"] = (object?)item.IsPostalSame ?? DBNull.Value;
            row["Address1"] = (object?)item.Address1 ?? DBNull.Value;
            row["Address2"] = (object?)item.Address2 ?? DBNull.Value;
            row["Address3"] = (object?)item.Address3 ?? DBNull.Value;
            row["Pos_PostalCode"] = (object?)item.Pos_PostalCode ?? DBNull.Value;
            row["HomeNumber"] = (object?)item.HomeNumber ?? DBNull.Value;
            row["CellNumber"] = (object?)item.CellNumber ?? DBNull.Value;
            row["Email"] = (object?)item.Email ?? DBNull.Value;
            row["StartDate"] = (object?)item.StartDate ?? DBNull.Value;
            row["IncomeTaxNumber"] = (object?)item.IncomeTaxNumber ?? DBNull.Value;
            row["NatureofPersonId"] = (object?)item.NatureofPersonId ?? DBNull.Value;
            row["DesignationId"] = (object?)item.DesignationId ?? DBNull.Value;
            row["PayrollSetupId"] = (object?)item.PayrollSetupId ?? DBNull.Value;
            row["flagEndEmployment"] = (object?)item.FlagEndEmployment ?? DBNull.Value; // Note: Typo in class? FlagEndEmployment
            row["EndEmploymentDate"] = (object?)item.EndEmploymentDate ?? DBNull.Value;
            row["ProfileId"] = (object?)item.ProfileId ?? DBNull.Value;
            row["EmploymentStatus"] = (object?)item.EmploymentStatus ?? DBNull.Value;
            row["PaymentTypeId"] = (object?)item.PaymentTypeId ?? DBNull.Value;
            row["Bank1Id"] = (object?)item.Bank1Id ?? DBNull.Value;
            row["AccountTypeId"] = (object?)item.AccountTypeId ?? DBNull.Value;
            row["AccountName"] = (object?)item.AccountName ?? DBNull.Value;
            row["AccountNumber"] = (object?)item.AccountNumber ?? DBNull.Value;
            row["Branch1Code"] = (object?)item.Branch1Code ?? DBNull.Value;
            row["SplitAmount1"] = (object?)item.SplitAmount1 ?? DBNull.Value;
            row["SplitPercentage1"] = (object?)item.SplitPercentage1 ?? DBNull.Value;
            row["IsJointAccount"] = (object?)item.IsJointAccount ?? DBNull.Value;
            row["AccountHolderRelationship"] = (object?)item.AccountHolderRelationship ?? DBNull.Value;
            row["Branch2Code"] = (object?)item.Branch2Code ?? DBNull.Value;
            row["Bank2Id"] = (object?)item.Bank2Id ?? DBNull.Value;
            row["AccountName1"] = (object?)item.AccountName1 ?? DBNull.Value;
            row["AccountNumber1"] = (object?)item.AccountNumber1 ?? DBNull.Value;
            row["AccountTypeId1"] = (object?)item.AccountTypeId1 ?? DBNull.Value;
            row["SplitAmount2"] = (object?)item.SplitAmount2 ?? DBNull.Value;
            row["SplitPercentage2"] = (object?)item.SplitPercentage2 ?? DBNull.Value;
            row["IsJointAccount1"] = (object?)item.IsJointAccount1 ?? DBNull.Value;
            row["AccountHolderRelationship1"] = (object?)item.AccountHolderRelationship1 ?? DBNull.Value;
            row["[Name]"] = (object?)item.Name ?? DBNull.Value;
            row["Relationship"] = (object?)item.Relationship ?? DBNull.Value;
            row["Phone"] = (object?)item.Phone ?? DBNull.Value;
            row["DepartmentId"] = (object?)item.DepartmentId ?? DBNull.Value;
            row["RaceId"] = (object?)item.RaceId ?? DBNull.Value;
            row["Title"] = (object?)item.Title ?? DBNull.Value;
            row["CopyCompanyAddress"] = (object?)item.CopyCompanyAddress ?? DBNull.Value;
            table.Rows.Add(row);
        }

        return table;
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

    public async Task<string?> UpsertESSUser(UpsertESSUserRequestDTO request)
    {
        var table = ESSUserToDataTable(request.Data);

        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@TemplateName", request.TemplateName);
        parameters.Add("@ImportFileName", request.ImportFileName);
        parameters.Add("@ESSUsers", table.AsTableValuedParameter("ESSUserImportType"));

        var result = await _dapper.ExecuteStoredProcedureSingle<string>(
            "usp_UpsertESSUser",
            parameters
        );

        return result == "SUCCESS" ? null : result;
    }

    private DataTable ESSUserToDataTable(IEnumerable<ESSUserData> data)
    {
        var table = new DataTable();
        table.Columns.Add("EmployeeCode", typeof(string));
        table.Columns.Add("Email", typeof(string));
        table.Columns.Add("Password", typeof(string));
        table.Columns.Add("PasswordHash", typeof(string));
        table.Columns.Add("AccessRoleName", typeof(string));
        table.Columns.Add("ApprovalManager", typeof(string));
        table.Columns.Add("SecondApproval", typeof(string));
        table.Columns.Add("IsManager", typeof(bool));
        table.Columns.Add("ApplyLeaveView", typeof(bool));
        table.Columns.Add("ApplyLeaveSave", typeof(bool));
        table.Columns.Add("ApplyLeaveDelete", typeof(bool));
        table.Columns.Add("PayslipsView", typeof(bool));
        table.Columns.Add("PayslipsSave", typeof(bool));
        table.Columns.Add("PayslipsDelete", typeof(bool));
        table.Columns.Add("ManageLeaveView", typeof(bool));
        table.Columns.Add("ManageLeaveSave", typeof(bool));
        table.Columns.Add("ManageLeaveDelete", typeof(bool));
        table.Columns.Add("ClaimsOnBehalfView", typeof(bool));
        table.Columns.Add("ClaimsOnBehalfSave", typeof(bool));
        table.Columns.Add("ClaimsOnBehalfDelete", typeof(bool));
        table.Columns.Add("ESSEMPLOYEESView", typeof(bool));
        table.Columns.Add("ESSEMPLOYEESSave", typeof(bool));
        table.Columns.Add("ESSEMPLOYEESDelete", typeof(bool));

        foreach (var item in data)
        {
            var row = table.NewRow();
            row["EmployeeCode"] = (object?)item.EmployeeCode ?? DBNull.Value;
            row["Email"] = (object?)item.Email ?? DBNull.Value;
            row["Password"] = (object?)item.Password ?? DBNull.Value;
            row["PasswordHash"] = (object?)item.PasswordHash ?? DBNull.Value;
            row["AccessRoleName"] = (object?)item.AccessRoleName ?? DBNull.Value;
            row["ApprovalManager"] = (object?)item.ApprovalManager ?? DBNull.Value;
            row["SecondApproval"] = (object?)item.SecondApproval ?? DBNull.Value;
            row["IsManager"] = (object?)item.IsManager ?? DBNull.Value;
            row["ApplyLeaveView"] = (object?)item.ApplyLeaveView ?? DBNull.Value;
            row["ApplyLeaveSave"] = (object?)item.ApplyLeaveSave ?? DBNull.Value;
            row["ApplyLeaveDelete"] = (object?)item.ApplyLeaveDelete ?? DBNull.Value;
            row["PayslipsView"] = (object?)item.PayslipsView ?? DBNull.Value;
            row["PayslipsSave"] = (object?)item.PayslipsSave ?? DBNull.Value;
            row["PayslipsDelete"] = (object?)item.PayslipsDelete ?? DBNull.Value;
            row["ManageLeaveView"] = (object?)item.ManageLeaveView ?? DBNull.Value;
            row["ManageLeaveSave"] = (object?)item.ManageLeaveSave ?? DBNull.Value;
            row["ManageLeaveDelete"] = (object?)item.ManageLeaveDelete ?? DBNull.Value;
            row["ClaimsOnBehalfView"] = (object?)item.ClaimsOnBehalfView ?? DBNull.Value;
            row["ClaimsOnBehalfSave"] = (object?)item.ClaimsOnBehalfSave ?? DBNull.Value;
            row["ClaimsOnBehalfDelete"] = (object?)item.ClaimsOnBehalfDelete ?? DBNull.Value;
            row["ESSEMPLOYEESView"] = (object?)item.ESSEMPLOYEESView ?? DBNull.Value;
            row["ESSEMPLOYEESSave"] = (object?)item.ESSEMPLOYEESSave ?? DBNull.Value;
            row["ESSEMPLOYEESDelete"] = (object?)item.ESSEMPLOYEESDelete ?? DBNull.Value;
            table.Rows.Add(row);
        }

        return table;
    }
}
