using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.Services.Utilities.UploadIRP5s;

namespace PalladiumPayroll.Controllers.Utilities;

[ApiController]
[Route("api/[controller]")]

public class UploadIRP5sController : ControllerBase
{
    private readonly IUploadIRP5sService _uploadIRP5SService;

    public UploadIRP5sController(IUploadIRP5sService uploadIRP5SService)
    {
        _uploadIRP5SService = uploadIRP5SService;
    }
}
