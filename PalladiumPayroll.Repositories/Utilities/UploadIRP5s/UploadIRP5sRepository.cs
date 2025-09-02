using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;

namespace PalladiumPayroll.Repositories.Utilities.UploadIRP5s;

public class UploadIRP5sRepository : IUploadIRP5sRepository
{
    private readonly DapperContext _dapper;

    public UploadIRP5sRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }
}
