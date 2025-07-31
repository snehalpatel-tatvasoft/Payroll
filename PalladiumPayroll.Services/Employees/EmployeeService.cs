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
            if (res)
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
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, string.Concat(ResponseMessages.Employee, " ", " Payment Detail"), ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeeWorkDropDown(int companyId)
        {
            return await _employeeRepository.GetEmployeeWorkDropDown(companyId);
        }

        public async Task<JsonResult> GetEmployeeWorkInformation(int employeeId)
        {
            return await _employeeRepository.GetEmployeeWorkInformation(employeeId);
        }

        public async Task<JsonResult> EmployeeWorkInfoSave(EmployeeWorkInformation reqModel)
        {
            reqModel.WorkingDay.Sort();
            var res = await _employeeRepository.EmployeeWorkInfoSave(reqModel);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, string.Concat(ResponseMessages.Employee, " ", " Work Information"), ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeeWorkOrganizationalDropdownData(long companyId)
        {
            return await _employeeRepository.GetEmployeeWorkOrganizationalDropdownData(companyId);
        }

        public async Task<JsonResult> GetCasualWageInformation(int employeeId)
        {
            return await _employeeRepository.GetCasualWageInformation(employeeId);
        }

        public async Task<JsonResult> UpdateCasualWageInformation(CasualWageInformation reqModel)
        {
            var result = await _employeeRepository.UpdateCasualWageInformation(reqModel);
            if (result == true)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, string.Concat(ResponseMessages.Employee, " ", "Casual Wage Information"), ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
        public async Task<TransactionTypeDropdownsDTO> GetTransactionTypesDropdownData(long companyId)
        {
            try
            {
                var data = await _employeeRepository.GetTransactionTypesDropdownData(companyId);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
