using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Utilities.DataImport;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Utilities.DataImport;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Utilities.DataImport;

public class DataImportService : IDataImportService
{
    private readonly IDataImportRepository _dataImportRepository;

    public DataImportService(IDataImportRepository dataImportRepository)
    {
        _dataImportRepository = dataImportRepository;
    }

    public async Task<JsonResult> GetPayrollProcessingTransactionsByCompany(PayrollTransactionFilterViewModel reqModel)
    {
        TableDataModel<PayrollProcessingTransactionDto> transactions = await _dataImportRepository.GetPayrollProcessingTransactionsByCompany(reqModel);

        return HttpStatusCodeResponse.SuccessResponse(transactions, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Retrieved));
    }
    public async Task<JsonResult> EmployeeMasterfileImport(EmployeeMasterImportRequestDTO request)
    {

        var errorMessage = await _dataImportRepository.EmployeeMasterfileImport(request);

        if (!string.IsNullOrEmpty(errorMessage))
            return HttpStatusCodeResponse.InternalServerErrorResponse(errorMessage);

        return HttpStatusCodeResponse.SuccessResponse(string.Empty, "Employees imported successfully.");

    }

    public async Task<JsonResult> UpsertESSUser(UpsertESSUserRequestDTO request)
    {
        try
        {
            if (request == null || request.CompanyId <= 0 || request.Data == null || !request.Data.Any())
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }

            // Hash passwords using PasswordHasher
            var passwordHasher = new PasswordHasher<object>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions
            {
                CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3,
                IterationCount = 10000 
            }));

            foreach (var user in request.Data)
            {
                if (string.IsNullOrWhiteSpace(user.Password))
                    continue;

                user.PasswordHash = passwordHasher.HashPassword(null, user.Password);
            }

            var result = await _dataImportRepository.UpsertESSUser(request);
            if (result == "ERROR")
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Failed to upsert ESS user.");
            }

            return HttpStatusCodeResponse.SuccessResponse(string.Empty, "ESS user upserted successfully.");
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse($"Error upserting ESS user: {ex.Message}");
        }
    }
}
