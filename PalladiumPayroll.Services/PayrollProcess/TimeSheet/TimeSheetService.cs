using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.TimeSheet;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.PayrollProcess.TimeSheet
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        public TimeSheetService(ITimeSheetRepository timeSheetRepository)
        {
            _timeSheetRepository = timeSheetRepository;
        }

        public async Task<JsonResult> GetLatestImportedData(int companyId)
        {
            var data = await _timeSheetRepository.GetLatestImportedData(companyId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Timesheet, ActionType.Retrieved));
        }

        public async Task<JsonResult> ImportTimeSheet(ImportTimeSheetRequest requestData)
        {
            var result = false;
            if (!requestData.IsHeader)
            {
                DataColumn[] sheetColumn =
                [
                    new DataColumn("EmployeeCode", typeof(string)),
                    new DataColumn("ClockInTime", typeof(DateTime)),
                    new DataColumn("ClockOutTime", typeof(DateTime)),
                ];
                var data = ExcelHelper.ImportFromExcel(requestData.File, false, sheetColumn);
                result = await _timeSheetRepository.ImportTimeSheetData(data);
            }
            else
            {
                var data = ExcelHelper.ImportFromExcel(requestData.File);
                result = await _timeSheetRepository.ImportTimeSheetData(data);
            }
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Timesheet, ActionType.Imported));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> ProcessTimeSheet()
        {
            var result = await _timeSheetRepository.ProcessTimeSheet();
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Timesheet, "Proccesed"));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
    }
}
