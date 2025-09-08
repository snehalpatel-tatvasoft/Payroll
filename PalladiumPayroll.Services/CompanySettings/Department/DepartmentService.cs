using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Department;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<JsonResult> GetDepartmentsByCompanyId(long companyId)
        {
            List<DepartmentResponseDTO>? departments = await _departmentRepository.GetDepartmentsByCompanyId(companyId);

            return HttpStatusCodeResponse.SuccessResponse(departments, string.Format(ResponseMessages.Success, ResponseMessages.Department, ActionType.Retrieved));
        }

        public async Task<JsonResult> CreateDepartment(DepartmentRequestDTO request)
        {
            bool exists = await _departmentRepository.CheckDepartmentNameExists(request.CompanyId, request.DepartmentName);
            if (exists)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.CheckDuplicateDepartment);
            }

            long departmentId = await _departmentRepository.CreateDepartment(request);
            if (departmentId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToCreateDepartment);
            }

            return HttpStatusCodeResponse.SuccessResponse(departmentId, string.Format(ResponseMessages.Success, ResponseMessages.Department, ActionType.Created));
        }

        public async Task<JsonResult> EditDepartment(long departmentId, DepartmentRequestDTO request)
        {
            List<DepartmentResponseDTO>? departments = await _departmentRepository.GetDepartmentsByCompanyId(request.CompanyId);

            DepartmentResponseDTO? existingDepartment = departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (existingDepartment != null)
            {
                bool exists = await _departmentRepository.CheckDepartmentNameExists(request.CompanyId, request.DepartmentName);

                if (exists)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.CheckDuplicateDepartment);
                }
            }

            bool success = await _departmentRepository.EditDepartment(departmentId, request);
            if (!success)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.ErrorUpdatingDepartment);
            }

            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Department, ActionType.Updated));
        }

        public async Task<JsonResult> DeleteDepartment(long departmentId,long? employeeId)
        {
            
                var (isSuccess, message) = await _departmentRepository.DeleteDepartment(departmentId, employeeId);
                if (!isSuccess)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(message);
                }

           return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Department, ActionType.Deleted));
        }
    }
}