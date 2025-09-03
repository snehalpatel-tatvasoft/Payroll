using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.PayrollProcess.PieceWork;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.PayrollProcess;


[ApiController]
[Route("api/[controller]")]
public class PieceWorkController : ControllerBase
{
    private readonly IPieceWorkService _pieceWorkService;

    public PieceWorkController(IPieceWorkService pieceWorkService)
    {
        _pieceWorkService = pieceWorkService;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetPieceWorkDropdownData(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _pieceWorkService.GetPieceWorkDropdownData(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.PieceWork+" DropList"));
        }
    }

    [HttpPost("[action]")]
    public async Task<JsonResult> AddPieceWorkDropdownItem(PieceWorkDropdownItem reqItem)
    {
        try
        {
            return await _pieceWorkService.AddPieceWorkDropdownItem(reqItem);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.PieceWork + " Droplist Item"));
        }
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> DeletePieceWorkDropdownItem(int id, int type)
    {
        try
        {
            return await _pieceWorkService.DeletePieceWorkDropdownItem(id,type);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.PieceWork + " Droplist Item")
            );
        }
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult> UpsertPieceWorkMasterData(UpsertPieceworkMasterDataDTO request)
    {
        try
        {
            return await _pieceWorkService.UpsertPieceWorkMasterData(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.PieceWorkMaster)
            );
        }
    }
}
