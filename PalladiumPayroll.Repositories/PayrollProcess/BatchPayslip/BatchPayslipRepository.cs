using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;

namespace PalladiumPayroll.Repositories.PayrollProcess.BatchPayslip
{
    public class BatchPayslipRepository : IBatchPayslipRepository
    {
        private readonly DapperContext _dapper;

        public BatchPayslipRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }
    }
}
