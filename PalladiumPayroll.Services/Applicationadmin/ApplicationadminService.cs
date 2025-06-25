using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using PalladiumPayroll.Helper.License;
using PalladiumPayroll.Repositories.Applicationadmin;
using ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Services.Applicationadmin
{
    public class ApplicationadminService:IApplicationadminService
    {
        private readonly IApplicationadminRepository _appAdminRepo;
        private readonly PayrollMachine _payrollMachine;
        public ApplicationadminService(IApplicationadminRepository appAdminRepo, PayrollMachine payrollMachine)
        {
            _appAdminRepo = appAdminRepo;
            _payrollMachine = payrollMachine;
        }
        public async Task<List<UserActivation>> GetUserActivationData(int mode)
        {
            var payrollMachineData = _payrollMachine.GetPayrollMachineData();
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(payrollMachineData.EndPoint);

            var client = new PalladWebWPFSoapClient(binding, endpoint);

            string licenceKey = "ZLC2LBFPVCT4";

            var webServiceResponse = await client.GetExpirationDateAsync(licenceKey);
            var data = await _appAdminRepo.GetUserActivationData(mode);
            return data;
        }
    }
}
