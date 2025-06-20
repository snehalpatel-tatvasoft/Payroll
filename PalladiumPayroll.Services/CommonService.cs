using PalladiumPayroll.Repositories;

namespace PalladiumPayroll.Services
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        public CommonService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

    }
}
