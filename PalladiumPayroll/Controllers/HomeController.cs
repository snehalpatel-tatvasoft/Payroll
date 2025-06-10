using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.Services.Home;

namespace PalladiumPayroll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        IHomeService _context;
        public HomeController(IHomeService _context) 
        { 
            this._context = _context;   
        }
        [HttpGet("GetAllEmployeeList")]
        public IActionResult GetAllEmployeeList()
        {
            List<Employee> empList = _context.GetAllEmployeeList();
            return Ok(empList);
        }
    }
}
