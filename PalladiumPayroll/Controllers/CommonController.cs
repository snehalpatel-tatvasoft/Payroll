using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.Services;

namespace PalladiumPayroll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }
    }
}
