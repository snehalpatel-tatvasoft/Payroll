using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Services.Applicationadmin
{
    public interface IApplicationadminService
    {
        Task<List<UserActivation>> GetUserActivationData(int mode);
    }
}
