using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeCodes;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.CompanySettings;

public class EmployeeCodesService : IEmployeeCodesService
{
    private readonly IEmployeeCodesRepository _employeeCodeRepository;

    public EmployeeCodesService(IEmployeeCodesRepository employeeCodeRepository)
    {
        _employeeCodeRepository = employeeCodeRepository;
    }

    public async Task<JsonResult> SaveEmployeeCodeGeneration(EmployeeCodeRequestDTO request)
    {
        try
        {
            bool isSaved = await _employeeCodeRepository.SaveEmployeeCodeGeneration(request);

            if (!isSaved)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeCodeSaveFailed);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeCode, ActionType.Saved));

        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetEmployeeCodeByCompanyId(int companyId)
    {
        try
        {
            EmployeeCodeResponseDTO? employeeCodeData = await _employeeCodeRepository.GetEmployeeCodeByCompanyId(companyId);

            if (employeeCodeData == null)
            {
                employeeCodeData = new EmployeeCodeResponseDTO
                {
                    IsAutomatic = true,
                    InitialCode = string.Empty
                };
            }

            return HttpStatusCodeResponse.SuccessResponse(employeeCodeData,string.Empty);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }
}
