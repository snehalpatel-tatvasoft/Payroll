using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;

namespace PalladiumPayroll.Services.PayrollProcess.PieceWork;

public interface IPieceWorkService
{
    Task<JsonResult> GetPieceWorkDropdownData(long companyId);

    Task<JsonResult> AddPieceWorkDropdownItem(PieceWorkDropdownItem reqItem);

    Task<JsonResult> DeletePieceWorkDropdownItem(int id, int type);

    Task<JsonResult> UpsertPieceWorkMasterData(UpsertPieceworkMasterDataDTO request);
}
