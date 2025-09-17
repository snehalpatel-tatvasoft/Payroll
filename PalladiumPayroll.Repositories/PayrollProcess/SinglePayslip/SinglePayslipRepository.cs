using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;

namespace PalladiumPayroll.Repositories.PayrollProcess.SinglePayslip;

public class SinglePayslipRepository : ISinglePayslipRepository
{
    private readonly DapperContext _dapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SinglePayslipRepository(IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
    {
        _dapper = new DapperContext(configuration);
        _httpContextAccessor = httpContextAccessor;
    }


}
