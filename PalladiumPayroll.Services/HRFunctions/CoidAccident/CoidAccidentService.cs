using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.CoidAccident;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.HRFunctions.CoidAccident;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;


namespace PalladiumPayroll.Services.HRFunctions.CoidAccident;

public class CoidAccidentService : ICoidAccidentService
{
    private readonly ICoidAccidentRepository _coidAccidentRepository;

    public CoidAccidentService(ICoidAccidentRepository coidAccidentRepository)
    {
        _coidAccidentRepository = coidAccidentRepository;
    }

    public async Task<JsonResult> UpsertCOIDAccident(CoidAccidentRequestDTO request)
    {
        var result = await _coidAccidentRepository.UpsertCOIDAccident(request);
        if (result)
        {
            string message = request.CoidAccidentId.HasValue
                ? ResponseMessages.CoidAccidentUpdatedSuccessfully
                : ResponseMessages.CoidAccidentCreatedSuccessfully;
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, message);
        }

        return HttpStatusCodeResponse.InternalServerErrorResponse(
            request.CoidAccidentId.HasValue
                ? ResponseMessages.CoidAccidentUpdateFailed
                : ResponseMessages.CoidAccidentCreateFailed);

    }
    public async Task<JsonResult> GetAccidentsByCompanyId(long companyId)
    {

        var result = await _coidAccidentRepository.GetAccidentsByCompanyId(companyId);
        return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.CoidAccident, ActionType.Retrieved));
 
    }
    public async Task<JsonResult> DeleteCoidAccident(long coidAccidentId)
    {
        var result = await _coidAccidentRepository.DeleteCoidAccident(coidAccidentId);
        if (result)
        {
            return HttpStatusCodeResponse.SuccessResponse(
                string.Empty,
                string.Format(ResponseMessages.Success, ResponseMessages.CoidAccident, ActionType.Deleted));
        }

        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.CoidAccidentFailedDelete);
    }

}
