using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.Repositories.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.Services.HRFunctions.DisciplinaryLog;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using PalladiumPayroll.DTOs.Miscellaneous;

namespace PalladiumPayroll.Services.DisciplinaryLog
{
    public class DisciplinaryLogService : IDisciplinaryLogService
    {
        private readonly IDisciplinaryLogRepository _disciplinaryLogRepository;
        private readonly string _fileUploadPath = @"D:\Payroll Project\Payroll-UI\Payroll-UI\src\assets\disciplinary-log-documents\";

        public DisciplinaryLogService(IDisciplinaryLogRepository disciplinaryLogRepository)
        {
            _disciplinaryLogRepository = disciplinaryLogRepository;
        }

        public async Task<JsonResult> GetDisciplinaryLogByCompanyId(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var logs = await _disciplinaryLogRepository.GetDisciplinaryLogByCompanyId(companyId);
                if (logs.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(logs, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Disciplinary Logs");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching disciplinary logs: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetEmployeesForDisciplinaryLogDropdown(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var employees = await _disciplinaryLogRepository.GetEmployeesForDisciplinaryLogDropdown(companyId);
                if (employees.Any())
                {
                    return HttpStatusCodeResponse.SuccessResponse(employees, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Employees");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching employees: {ex.Message}");
            }
        }

        public async Task<JsonResult> CreateDisciplinaryLog(DisciplinaryLogRequestDTO disciplinaryLog, IFormFile file)
        {
            try
            {
                if (disciplinaryLog.CompanyId <= 0 || disciplinaryLog.EmployeeId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(_fileUploadPath, fileName);

                    if (!Directory.Exists(_fileUploadPath))
                    {
                        Directory.CreateDirectory(_fileUploadPath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    disciplinaryLog.FileName = file.FileName;
                    disciplinaryLog.FilePath = filePath;
                }

                var disciplinaryLogId = await _disciplinaryLogRepository.CreateDisciplinaryLog(disciplinaryLog);
                if (disciplinaryLogId > 0)
                {
                    return HttpStatusCodeResponse.SuccessResponse(new { DisciplinaryLogId = disciplinaryLogId }, string.Format(ResponseMessages.Success, "Disciplinary Log", "Created"));
                }
                return HttpStatusCodeResponse.BadRequestResponse();
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error creating disciplinary log: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetDisciplinaryLogById(long disciplinaryLogId)
        {
            try
            {
                if (disciplinaryLogId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var log = await _disciplinaryLogRepository.GetDisciplinaryLogById(disciplinaryLogId);
                if (log != null)
                {
                    return HttpStatusCodeResponse.SuccessResponse(log, ResponseMessages.DataFetchSuccess);
                }
                return HttpStatusCodeResponse.NotFoundResponse("Disciplinary Log");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching disciplinary log: {ex.Message}");
            }
        }

        public async Task<JsonResult> UpdateDisciplinaryLog(DisciplinaryLogEditRequestDTO disciplinaryLog, IFormFile file)
        {
            try
            {
                if (disciplinaryLog.DisciplinaryLogId <= 0 || disciplinaryLog.CompanyId <= 0 || disciplinaryLog.EmployeeId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(_fileUploadPath, fileName);

                    if (!Directory.Exists(_fileUploadPath))
                    {
                        Directory.CreateDirectory(_fileUploadPath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    disciplinaryLog.FileName = file.FileName;
                    disciplinaryLog.FilePath = filePath;
                }

                var rowsAffected = await _disciplinaryLogRepository.UpdateDisciplinaryLog(disciplinaryLog);
                if (rowsAffected > 0)
                {
                    return HttpStatusCodeResponse.SuccessResponse(true, string.Format(ResponseMessages.Success, "Disciplinary Log", "Updated"));
                }
                return HttpStatusCodeResponse.NotFoundResponse("Disciplinary Log");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error updating disciplinary log: {ex.Message}");
            }
        }

        public async Task<JsonResult> DeleteDisciplinaryLog(long disciplinaryLogId)
        {
            try
            {
                if (disciplinaryLogId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                var rowsAffected = await _disciplinaryLogRepository.DeleteDisciplinaryLog(disciplinaryLogId);
                if (rowsAffected > 0)
                {
                    return HttpStatusCodeResponse.SuccessResponse(true, string.Format(ResponseMessages.Success, "Disciplinary Log", "Deleted"));
                }
                return HttpStatusCodeResponse.NotFoundResponse("Disciplinary Log");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error deleting disciplinary log: {ex.Message}");
            }
        }
    }
}