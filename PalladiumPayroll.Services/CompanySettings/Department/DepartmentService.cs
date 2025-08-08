using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Department;
using static PalladiumPayroll.Helper.Constants.AppConstants;

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
            try
            {
                var departments = await _departmentRepository.GetDepartmentsByCompanyId(companyId);

                if (departments == null)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(
                        ResponseMessages.NoDepartmentsForThisCompanyId
                    );
                }

                return HttpStatusCodeResponse.SuccessResponse(
                    data: departments,
                    ResponseMessages.DepartmentsRetrievedSuccessfully
                );
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    ResponseMessages.ErrorRetrievingDepartments
                );
            }
        }

        public async Task<JsonResult> CreateDepartment(DepartmentRequestDTO request)
        {
            try
            {
                string normalizedName = request.DepartmentName?.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(normalizedName))
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                bool exists = await _departmentRepository.CheckDepartmentNameExists(request.CompanyId, normalizedName);
                if (exists)
                {
                    return HttpStatusCodeResponse.GenerateResponse(
                        result: false,
                        statusCode: HttpStatusCode.Conflict,
                        ResponseMessages.CheckDuplicateDepartment,
                        data: string.Empty
                    );
                }

                long departmentId = await _departmentRepository.CreateDepartment(request);
                if (departmentId <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(
                        ResponseMessages.ErrorCreatingDepartment
                    );
                }

                return HttpStatusCodeResponse.SuccessResponse(
                    data: departmentId,
                    message: ResponseMessages.DepartmentCreateSuccessfully
                );
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    ResponseMessages.ErrorCreatingDepartment
                );
            }
        }

        public async Task<JsonResult> EditDepartment(long departmentId, DepartmentRequestDTO request)
        {
            try
            {
                string normalizedName = request.DepartmentName?.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(normalizedName))
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                // Fetch the current department to exclude it from the duplicate check
                var departments = await _departmentRepository.GetDepartmentsByCompanyId(request.CompanyId);
                var existingDepartment = departments.FirstOrDefault(d => d.DepartmentId == departmentId);
                if (existingDepartment != null)
                {
                    bool exists = await _departmentRepository.CheckDepartmentNameExists(request.CompanyId, normalizedName);
                    if (exists && existingDepartment.DepartmentName?.Trim().ToLower() != normalizedName)
                    {
                        return HttpStatusCodeResponse.GenerateResponse(
                            result: false,
                            statusCode: HttpStatusCode.Conflict,
                            ResponseMessages.CheckDuplicateDepartment,
                            data: string.Empty
                        );
                    }
                }

                bool success = await _departmentRepository.EditDepartment(departmentId, request);
                if (!success)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(
                        ResponseMessages.ErrorUpdatingDepartment
                    );
                }

                return HttpStatusCodeResponse.SuccessResponse(
                    data: departmentId,
                    ResponseMessages.DepartmentUpdateSuccessfully
                );
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    ResponseMessages.ErrorUpdatingDepartment
                );
            }
        }

        public async Task<JsonResult> DeleteDepartment(long departmentId)
        {
            try
            {
                bool success = await _departmentRepository.DeleteDepartment(departmentId);
                if (!success)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(
                        ResponseMessages.ErrorDeletingDepartment
                    );
                }

                return HttpStatusCodeResponse.SuccessResponse(
                    data: string.Empty,
                    ResponseMessages.DepartmentDeleteSuccessfully
                );
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                    ResponseMessages.ErrorDeletingDepartment
                );
            }
        }
    }
}