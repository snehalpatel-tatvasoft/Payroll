using PalladiumPayroll.Repositories.Utilities.UploadIRP5s;

namespace PalladiumPayroll.Services.Utilities.UploadIRP5s;

public class UploadIRP5sService : IUploadIRP5sService
{
    private readonly IUploadIRP5sRepository _uploadIRP5SRepository;

    public UploadIRP5sService(IUploadIRP5sRepository uploadIRP5SRepository)
    {
        _uploadIRP5SRepository = uploadIRP5SRepository;
    }
}
