using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeGrievances;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeGrievances;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.HRFunctions.EmployeeGrievances;

public class EmployeeGrievancesService : IEmployeeGrievancesService
{
    private readonly IEmployeeGrievancesRepository _employeeGrievancesRepository;

    public EmployeeGrievancesService(IEmployeeGrievancesRepository employeeGrievancesRepository)
    {
        _employeeGrievancesRepository = employeeGrievancesRepository;
    }

    public async Task<JsonResult> UpsertEmployeeGrievance(EmployeeGrievanceUpsertData request)
    {
        try
        {
            bool isSaved = await _employeeGrievancesRepository.UpsertEmployeeGrievance(request);

            if (!isSaved)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeGrievanceSaveFailed);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeGrievances, ActionType.Saved));

        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> DeleteEmployeeGrievance(long employeeGrievanceId, string userId)
    {
        try
        {
            bool isDeleted = await _employeeGrievancesRepository.DeleteEmployeeGrievance(employeeGrievanceId, userId);

            if (!isDeleted)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeGrievanceNotFound);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeGrievances, ActionType.Deleted));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }

    }

    public async Task<JsonResult> GetEmployeesForGrievances(long companyId)
    {
        try
        {
            List<EmployeeDropdownDTO> employees = await _employeeGrievancesRepository.GetEmployeesForGrievances(companyId);

            return HttpStatusCodeResponse.SuccessResponse(employees, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetNatureOfGrievances(long companyId)
    {
        try
        {
            List<NatureOfGrievanceDTO> natureOfGrievances = await _employeeGrievancesRepository.GetNatureOfGrievances(companyId);

            return HttpStatusCodeResponse.SuccessResponse(natureOfGrievances, string.Format(ResponseMessages.Success, ResponseMessages.NatureOfGrievances, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetEmployeeGrievances(long companyId)
    {
        try
        {
            List<EmployeeGrievanceDTO> employeeGrievances = await _employeeGrievancesRepository.GetEmployeeGrievances(companyId);

            return HttpStatusCodeResponse.SuccessResponse(employeeGrievances, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeGrievances, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

     public async Task<JsonResult> GetEmployeeGrievanceById(long grievanceId)
    {
        try
        {
            EmployeeGrievanceDTO? employeeGrievance = await _employeeGrievancesRepository.GetEmployeeGrievanceById(grievanceId);

            return HttpStatusCodeResponse.SuccessResponse(employeeGrievance, string.Empty);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }
}
