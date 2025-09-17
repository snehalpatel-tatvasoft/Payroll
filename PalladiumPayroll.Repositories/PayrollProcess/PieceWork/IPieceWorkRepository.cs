using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;

namespace PalladiumPayroll.Repositories.PayrollProcess.PieceWork;

public interface IPieceWorkRepository
{
    Task<PieceWorkDropdownsDTO> GetPieceWorkDropdownData(long companyId);

    Task<List<DropDownViewModel>> AddPieceWorkDropdownItem(PieceWorkDropdownItem reqItem);

    Task<DeleteDropDownResult> DeletePieceWorkDropdownItem(int id, int type);

    Task<bool> UpsertPieceWorkMasterData(UpsertPieceworkMasterDataDTO request);

    Task<decimal?> GetPieceworkRate(GetPieceworkRateRequestDTO request);

    Task<bool> UpsertPieceWork(UpsertPieceworkDTO request);

     Task<TableDataModel<PieceworkListDTO>> GetPieceWorkList(PieceWorkFilterViewModel reqModel);

      Task<bool> DeletePieceWork(int pieceWorkId);
}
