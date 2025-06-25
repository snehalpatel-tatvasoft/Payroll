using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PalladiumPayroll.DTOs.DTOs;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
