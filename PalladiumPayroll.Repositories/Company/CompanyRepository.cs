using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;

namespace PalladiumPayroll.Repositories.Company
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _dapper;
        public CompanyRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

    }
}
