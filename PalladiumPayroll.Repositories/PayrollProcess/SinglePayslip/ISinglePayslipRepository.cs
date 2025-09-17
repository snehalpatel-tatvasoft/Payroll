using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

namespace PalladiumPayroll.Repositories.PayrollProcess.SinglePayslip;

public interface ISinglePayslipRepository
{
    Task<List<PayrollCycleDropdownDTO>> GetPayrollCycleDropdown(long companyId);

    Task<string?> GetNextUnprocessedPeriod(long companyId, long companyPayrollId);
}
