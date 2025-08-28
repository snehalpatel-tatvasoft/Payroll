using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public class ManageLeaveService : IManageLeaveService
    {
        private readonly IManageLeaveRepository _manageLeaveRepository;
        public ManageLeaveService(IManageLeaveRepository manageLeaveRepository)
        {
            _manageLeaveRepository = manageLeaveRepository;
        }

        public async Task<JsonResult> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel)
        {
            var data =  await _manageLeaveRepository.GetEmployeeLeaveDetail(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Leave", ActionType.Retrieved));
        }
    }
}
