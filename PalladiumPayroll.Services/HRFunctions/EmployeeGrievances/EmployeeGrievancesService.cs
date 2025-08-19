using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
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
        bool isSaved = await _employeeGrievancesRepository.UpsertEmployeeGrievance(request);

        if (!isSaved)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmployeeGrievanceSaveFailed);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeGrievances, ActionType.Saved));
    }

    public async Task<JsonResult> DeleteEmployeeGrievance(long employeeGrievanceId)
    {
        bool isDeleted = await _employeeGrievancesRepository.DeleteEmployeeGrievance(employeeGrievanceId);

        if (!isDeleted)
        {
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeGrievanceNotFound);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeGrievances, ActionType.Deleted));
    }

    public async Task<JsonResult> GetEmployeesForGrievances(long companyId)
    {
        List<EmployeeDropdownDTO> employees = await _employeeGrievancesRepository.GetEmployeesForGrievances(companyId);
        return HttpStatusCodeResponse.SuccessResponse(employees, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieved));
    }

    public async Task<JsonResult> GetNatureOfGrievances(long companyId)
    {
        List<NatureOfGrievanceDTO> natureOfGrievances = await _employeeGrievancesRepository.GetNatureOfGrievances(companyId);
        return HttpStatusCodeResponse.SuccessResponse(natureOfGrievances, string.Format(ResponseMessages.Success, ResponseMessages.NatureOfGrievances, ActionType.Retrieved));
    }

    public async Task<JsonResult> GetEmployeeGrievances(long companyId)
    {
        List<EmployeeGrievanceDTO> employeeGrievances = await _employeeGrievancesRepository.GetEmployeeGrievances(companyId);
        return HttpStatusCodeResponse.SuccessResponse(employeeGrievances, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeGrievances, ActionType.Retrieved));
    }

    public async Task<JsonResult> GetEmployeeGrievanceById(long grievanceId)
    {
        EmployeeGrievanceDTO? employeeGrievance = await _employeeGrievancesRepository.GetEmployeeGrievanceById(grievanceId);
        return HttpStatusCodeResponse.SuccessResponse(employeeGrievance, string.Empty);
    }

    public async Task<JsonResult> AddNatureOfGrievance(NatureOfGrievancesDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return HttpStatusCodeResponse.NotFoundResponse("Name is required.");

        List<DropDownViewModel>? result = await _employeeGrievancesRepository.AddNatureOfGrievance(request);

        if (result.Count == 1 && result[0].Id == -1 && result[0].Value == "Duplicate")
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("This Nature of Grievance already exists for the selected company.");
        }
        return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.NatureOfGrievances, ActionType.Saved));
    }

    public async Task<JsonResult> DeleteNatureOfGrievance(int natureOfGrievanceId)
    {
        bool isDeleted = await _employeeGrievancesRepository.DeleteNatureOfGrievance(natureOfGrievanceId);
        if (!isDeleted)
        {
            return HttpStatusCodeResponse.NotFoundResponse("Nature of Grievance not found or already deleted.");
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.NatureOfGrievances, ActionType.Deleted));
    }
}
