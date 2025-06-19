using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;

namespace PalladiumPayroll.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _dapper;

        public EmployeeRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }
    }
}
