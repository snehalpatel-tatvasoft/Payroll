using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.Services.PayrollProcess.SinglePayslip;

namespace PalladiumPayroll.Controllers.PayrollProcess;

[Route("api/[controller]")]
[ApiController]
public class SinglePayslipController : ControllerBase
{
    private readonly ISinglePayslipService _singlePayslipService;
    public SinglePayslipController(ISinglePayslipService singlePayslipService)
    {
        _singlePayslipService = singlePayslipService;
    }
}
