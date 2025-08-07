using System.Net;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.EmployeesLoan;
using PalladiumPayroll.Services.EmployeesLoan;
using PalladiumPayroll.DTOs.Miscellaneous;
namespace PalladiumPayroll.Controllers.EmployeesLoan;

[ApiController]
[Route("api/[controller]")]
public class EmployeesLoanController : ControllerBase
{

    private readonly IEmployeesLoanService _service;

    public EmployeesLoanController(IEmployeesLoanService service)
    {
        _service = service;
    }

    [HttpPost("CreateEmployeeLoan")]
    public async Task<IActionResult> CreateEmployeeLoan([FromBody] EmployeeLoanRequestDTO request)
    {
        try
        {
            var result = await _service.CreateEmployeeLoan(request);
            return result;
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    [HttpPut("UpdateEmployeeLoan")]
    public async Task<ActionResult> EditEmployeeLoan([FromBody] EmployeeLoanRequestDTO request)
    {
        try
        {
            var res = await _service.UpdateEmployeeLoan(request);
            return res;
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPut("PauseLoan/{employeeLoanId}/{updatedBy}")]
    public async Task<ActionResult> PauseLoan(long employeeLoanId, long updatedBy)
    {
        try
        {
            var res = await _service.PauseEmployeeLoan(employeeLoanId, updatedBy);
            return res;
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPut("FullPaidLoan/{employeeLoanId}/{updatedBy}")]
    public async Task<ActionResult> FullPaidLoan(long employeeLoanId, long updatedBy)
    {
        try
        {
            var res = await _service.FullPaidEmployeeLoan(employeeLoanId, updatedBy);
            return res;
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet("GetLoansByCompany")]
    public async Task<ActionResult> GetLoansByCompany([FromQuery] LoanFilterViewModel reqModel)
    {
        try
        {
            var res = await _service.GetLoansByCompanyId(reqModel);
            return res;
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet("GetEmployeeLoanDropdowns")]
    public async Task<IActionResult> GetEmployeeLoanDropdowns(long companyId)
    {
        try
        {
            if (companyId <= 0)
                return HttpStatusCodeResponse.NotFoundResponse("Invalid company ID.");

            var response = await _service.GetEmployeeLoanDropdowns(companyId);
            return HttpStatusCodeResponse.SuccessResponse(response, "Employee Loan dropdowns retrieved successfully.");
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse($"Error retrieving dropdowns: {ex.Message}");
        }
    }




}
