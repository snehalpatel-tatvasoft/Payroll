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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.CreateTransaction, ex.Message)
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.CreateTransaction, ex.Message)
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Updating, ResponseMessages.CreateTransaction, ex.Message)
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.CreateTransaction, ex.Message)
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Deleting, ResponseMessages.CreateTransaction, ex.Message)
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.CreateTransaction, ex.Message)
            );
        }
    }

}
