using PalladiumPayroll.Repositories.PayrollProcess.SinglePayslip;

namespace PalladiumPayroll.Services.PayrollProcess.SinglePayslip;

public class SinglePayslipService : ISinglePayslipService
{
    private readonly ISinglePayslipRepository _singlePayslipRepository;
    public SinglePayslipService(ISinglePayslipRepository singlePayslipRepository)
    {
        _singlePayslipRepository = singlePayslipRepository;
    }
}
