using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Employees;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<JsonResult> GetEmployeeFilters(int companyId)
        {
            return await _employeeRepository.GetEmployeeFilters(companyId);
        }

        public async Task<JsonResult> GetEmployeeList(EmployeeFilterViewModel reqModel)
        {
            return await _employeeRepository.GetEmployeeList(reqModel);
        }

        public async Task<JsonResult> DeleteEmployee(int employeeId)
        {
            var res = await _employeeRepository.DeleteEmployee(employeeId);
            if (res == true)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeePaymentDetail(int employeeId)
        {
            return await _employeeRepository.GetEmployeePaymentDetail(employeeId);
        }

        public async Task<JsonResult> EmployeePaymentDetailSave(EmployeePaymentDetail reqModel)
        {
            var res = await _employeeRepository.EmployeePaymentDetailSave(reqModel);
            if (res == true)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, string.Concat(ResponseMessages.Employee, " ", " Payment Detail"), ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
    }
}
