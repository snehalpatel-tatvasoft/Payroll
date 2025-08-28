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

        public async Task<JsonResult> GetEmployeeLeave(int leaveDetailId)
        {
            var data = await _manageLeaveRepository.GetEmployeeLeave(leaveDetailId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Leave", ActionType.Retrieved));
        }

        public async Task<JsonResult> UpsertEmployeeLeave(AddEmployeeLeaves reqModel)
        {
            var res = await _manageLeaveRepository.UpsertEmployeeLeave(reqModel);
            if (res == 1)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Leave", ActionType.Updated));
            }
            else if(res == 2)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.AlreadyExist, "Leave date is"));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
    }
}
