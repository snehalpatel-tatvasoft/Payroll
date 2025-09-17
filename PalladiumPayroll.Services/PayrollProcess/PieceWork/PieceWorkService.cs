using System.Data;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.PieceWork;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper.ImportExport;
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

    public async Task<JsonResult> UpsertPieceWork(UpsertPieceworkDTO request)
    {
        bool isSaved = await _pieceWorkRepository.UpsertPieceWork(request);

        if (!isSaved)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.PieceWorkSaveFiled);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.PieceWork, ActionType.Saved));
    }

    public async Task<JsonResult> GetPieceWorkList(PieceWorkFilterViewModel reqModel)
    {
        TableDataModel<PieceworkListDTO> pieceworks = await _pieceWorkRepository.GetPieceWorkList(reqModel);

        return HttpStatusCodeResponse.SuccessResponse(pieceworks, string.Format(ResponseMessages.Success, ResponseMessages.PieceWork, ActionType.Retrieved));
    }

    public async Task<JsonResult> DeletePieceWork(int pieceWorkId)
    {
        bool isDeleted = await _pieceWorkRepository.DeletePieceWork(pieceWorkId);

        if (!isDeleted)
        {
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.PieceWork);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.PieceWork, ActionType.Deleted));
    }

    public async Task<byte[]> ExportPieceworkList(PieceWorkFilterViewModel reqModel)
    {
        reqModel.CurrentPage = 0;
        reqModel.PageSize = 0;

        TableDataModel<PieceworkListDTO>? data = await _pieceWorkRepository.GetPieceWorkList(reqModel);

        DataTable? dt = new DataTable();
        dt.Columns.Add("Employee Code", typeof(string));
        dt.Columns.Add("Employee Name", typeof(string));
        dt.Columns.Add("Product Type", typeof(string));
        dt.Columns.Add("Area", typeof(string));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("Rate", typeof(decimal));
        dt.Columns.Add("Quantity Delivered", typeof(decimal));
        dt.Columns.Add("Payment Date", typeof(DateTime));
        dt.Columns.Add("Total Paid Amount", typeof(decimal));
        dt.Columns.Add("Include SDL", typeof(bool));
        dt.Columns.Add("Include UIF", typeof(bool));
        dt.Columns.Add("Created Date", typeof(DateTime));

        foreach (var item in data.DataList)
        {
            dt.Rows.Add(
                item.EmployeeCode,
                item.EmployeeName,
                item.ProductType,
                item.Area,
                item.Unit,
                item.Rate ?? 0,
                item.QuantityDelivered ?? 0,
                item.PaymentDate == null ? DBNull.Value : item.PaymentDate,
                item.TotalPaidAmount ?? 0,
                item.InfluenceSDL,
                item.InfluenceUIF,
                item.CreatedDate == null ? DBNull.Value : item.CreatedDate
            );
        }
        string exportType = string.IsNullOrWhiteSpace(reqModel.ExportType) ? "Excel" : reqModel.ExportType;

        if (exportType.Equals("Pdf", StringComparison.OrdinalIgnoreCase))
        {
            DataTable pdfTable = dt.Clone();
            pdfTable.Columns["Payment Date"]!.DataType = typeof(string);
            pdfTable.Columns["Created Date"]!.DataType = typeof(string);

            foreach (DataRow row in dt.Rows)
            {
                pdfTable.Rows.Add(
                    row["Employee Code"],
                    row["Employee Name"],
                    row["Product Type"],
                    row["Area"],
                    row["Unit"],
                    row["Rate"],
                    row["Quantity Delivered"],
                    row["Payment Date"] == DBNull.Value ? "" : ((DateTime)row["Payment Date"]).ToString("dd-MM-yyyy"),
                    row["Total Paid Amount"],
                    row["Include SDL"],
                    row["Include UIF"],
                    row["Created Date"]== DBNull.Value ? "" : ((DateTime)row["Created Date"]).ToString("dd-MM-yyyy")
                );
            }
              return PdfHelper.ExportToPdfTable(pdfTable, 25);
        }
        else
        {
            return ExcelHelper.ExportToExcel(new Dictionary<string, DataTable> { { "Piecework", dt } });
        }
    }

}
