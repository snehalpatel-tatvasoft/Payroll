using Microsoft.Extensions.Options;

namespace PalladiumPayroll.Helper.License
{
    public class PayrollMachine
    {
        private readonly PayrollMachineData _settings;

        public PayrollMachine(IOptions<PayrollMachineData> options)
        {
            _settings = options.Value;
        }

        public PayrollMachineData GetPayrollMachineData()
        {
            return _settings;
        }
    }
    public class PayrollMachineData
    {
        public string? EndPoint { get; set; }
        public string? MachineName { get; set; }
        public string? HDDSerialNumber { get; set; }
    }
}
