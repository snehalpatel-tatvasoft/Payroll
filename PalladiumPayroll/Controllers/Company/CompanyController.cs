using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.Services.Company;

namespace PalladiumPayroll.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
    }
}
