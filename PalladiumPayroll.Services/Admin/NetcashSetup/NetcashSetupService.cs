using PalladiumPayroll.Repositories.Admin.NetcashSetup;

namespace PalladiumPayroll.Services.Admin.NetcashSetup;

public class NetcashSetupService : INetcashSetupService
{
    private readonly INetcashSetupRepository _netcashSetupRepository;

    public NetcashSetupService(INetcashSetupRepository netcashSetupRepository)
    {
        _netcashSetupRepository = netcashSetupRepository;
    }

}
