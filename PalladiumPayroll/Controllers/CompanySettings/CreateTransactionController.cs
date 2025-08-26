using System.Net;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.CreateTransaction;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings.CreateTransaction;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class CreateTransactionController : ControllerBase
{
    private readonly ICreateTransactionService _createTransactionService;

    public CreateTransactionController(ICreateTransactionService createTransactionService)
    {
        _createTransactionService = createTransactionService;
    }

    [HttpGet("Transactions")]
    public async Task<ActionResult> GetAllTransactions([FromQuery] long companyId)
    {
        try
        {
            var res = await _createTransactionService.GetAllTransactions(companyId);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CreateTransaction)
            );
        }
    }

    [HttpPost("AddTransaction")]
    public async Task<ActionResult> AddTransaction([FromBody] CreateTransactionRequestDTO request)
    {
        try
        {
            var res = await _createTransactionService.AddTransaction(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.CreateTransaction)
            );
        }
    }

    [HttpPut("UpdateTransaction/{id}")]
    public async Task<ActionResult> UpdateTransaction(long id, [FromBody] CreateTransactionRequestDTO request)
    {
        try
        {
            request.PayrollProcessId = id;
            var res = await _createTransactionService.UpdateTransaction(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.CreateTransaction)
            );
        }
    }

    [HttpGet("Transactions/{id}")]
    public async Task<ActionResult> GetTransactionById(long id)
    {
        try
        {
            var res = await _createTransactionService.GetTransactionById(id);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CreateTransaction)
            );
        }
    }

    [HttpDelete("DeleteTransaction/{id}")]
    public async Task<ActionResult> DeleteTransaction(long id)
    {
        try
        {
            JsonResult res = await _createTransactionService.DeleteTransaction(id);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.CreateTransaction)
            );
        }
    }

    [HttpPost("ImportTransactions")]
    public async Task<ActionResult> ImportTransactions([FromBody] ImportTransactionRequestDTO request)
    {
        try
        {
            var res = await _createTransactionService.ImportTransactions(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.CreateTransaction)
            );
        }
    }

}
