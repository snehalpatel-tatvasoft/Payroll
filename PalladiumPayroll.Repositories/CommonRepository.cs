using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
namespace PalladiumPayroll.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly DapperContext _dapper;
        public CommonRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

    }
}
