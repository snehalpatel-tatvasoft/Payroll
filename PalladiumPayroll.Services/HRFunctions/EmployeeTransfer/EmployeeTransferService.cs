using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.HRFunctions.EmployeeTransfer;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeTransfer;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.HRFunctions.EmployeeTransfer;

public class EmployeeTransferService : IEmployeeTransferService
{
    private readonly IEmployeeTransferRepository _employeeTransferRepository;

    public EmployeeTransferService(IEmployeeTransferRepository employeeTransferRepository)
    {
        _employeeTransferRepository = employeeTransferRepository;
    }

    public async Task<EmployeeTransferDropdownsDTO> GetEmployeeTransferDropdownData(long companyId)
    {
        try
        {
            var data = await _employeeTransferRepository.GetEmployeeTransferDropdownData(companyId);
            return data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<JsonResult> GetEmployeeAutoFillData(long employeeId, long companyId)
    {
        try
        {
            if (employeeId <= 0 || companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeOrCompanyIdInvalid);
            }

            var autofillData = await _employeeTransferRepository.GetEmployeeAutoFillData(employeeId, companyId);

            if (autofillData == null)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeNotFound);
            }

            return HttpStatusCodeResponse.SuccessResponse(autofillData, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTransfer, ActionType.Retrieved));
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


}