using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;

namespace PalladiumPayroll.Repositories.Admin.NetcashSetup;

public class NetcashSetupRepository : INetcashSetupRepository
{
    private readonly DapperContext _dapper;

    public NetcashSetupRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

}
