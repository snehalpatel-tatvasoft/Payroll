using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.PayrollProcess.PieceWork;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.PayrollProcess.PieceWork;

public class PieceWorkService : IPieceWorkService
{
    private readonly IPieceWorkRepository _pieceWorkRepository;

    public PieceWorkService(IPieceWorkRepository pieceWorkRepository)
    {
        _pieceWorkRepository = pieceWorkRepository;
    }

    public async Task<JsonResult> GetPieceWorkDropdownData(long companyId)
    {
        PieceWorkDropdownsDTO? data = await _pieceWorkRepository.GetPieceWorkDropdownData(companyId);

        return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.PieceWork + " DropList", ActionType.Retrieved));
    }


    public async Task<JsonResult> AddPieceWorkDropdownItem(PieceWorkDropdownItem reqItem)
    {
        List<DropDownViewModel> result = await _pieceWorkRepository.AddPieceWorkDropdownItem(reqItem);

        if (result.Count > 0 && result.FirstOrDefault()?.Id > 0)
        {
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, "Item", ActionType.Saved));
        }
        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
    }


    public async Task<JsonResult> DeletePieceWorkDropdownItem(int id, int type)
    {
        DeleteDropDownResult? result = await _pieceWorkRepository.DeletePieceWorkDropdownItem(id, type);

        if (!result.Success)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(result.Message);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, result.Message);
    }


    public async Task<JsonResult> UpsertPieceWorkMasterData(UpsertPieceworkMasterDataDTO request)
    {
        bool isSaved = await _pieceWorkRepository.UpsertPieceWorkMasterData(request);

        if (!isSaved)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.PieceWorkMasterSaveFiled);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.PieceWorkMaster, ActionType.Saved));
    }

    public async Task<JsonResult> GetPieceworkRate(GetPieceworkRateRequestDTO request)
    {
        decimal? rate = await _pieceWorkRepository.GetPieceworkRate(request);

        return HttpStatusCodeResponse.SuccessResponse(rate, string.Format(ResponseMessages.Success, ResponseMessages.PieceWork + " Rate", ActionType.Retrieved));
    }


}
