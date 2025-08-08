using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.Services.Utilities.DataImport;

namespace PalladiumPayroll.Controllers.Utilities;

[ApiController]
[Route("api/[controller]")]
public class DataImportController : ControllerBase
{
    private readonly IDataImportService _dataImportService;

    public DataImportController(IDataImportService dataImportService)
    {
        _dataImportService = dataImportService;
    }
}
