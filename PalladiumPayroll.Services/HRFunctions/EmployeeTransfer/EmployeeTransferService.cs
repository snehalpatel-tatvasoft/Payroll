using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTransfer;
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
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }
    public async Task<JsonResult> AddEmployeeTransfer(EmployeeTransferRequestDTO request)
    {
        try
        {
            if (request.EmployeeId <= 0 || request.CompanyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeOrCompanyId);
            }

            var result = await _employeeTransferRepository.AddEmployeeTransfer(request);
            if (result != null)
            {
                return HttpStatusCodeResponse.SuccessResponse(result, ResponseMessages.EmployeeTransferCreatedSuccessfully);
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmployeeTransferCreationFailed);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }
    public async Task<List<EmployeeTransferDisplayDataModel>> GetEmployeeTransferList(long companyId)
    {
        try
        {
            var data = await _employeeTransferRepository.GetEmployeeTransferList(companyId);
            return data;
        }
        catch (Exception)
        {
            throw;
        }
    }
}