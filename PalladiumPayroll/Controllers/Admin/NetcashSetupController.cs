using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.Services.Admin.NetcashSetup;

namespace PalladiumPayroll.Controllers.Admin;

[ApiController]
[Route("api/[controller]")]
public class NetcashSetupController : ControllerBase
{
    private readonly INetcashSetupService _netcashSetupService;

    public NetcashSetupController(INetcashSetupService netcashSetupService)
    {
        _netcashSetupService = netcashSetupService;
    }
}
