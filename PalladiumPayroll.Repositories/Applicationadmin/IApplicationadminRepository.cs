using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Repositories.Applicationadmin
{
    public interface IApplicationadminRepository
    {
        Task<List<UserActivation>> GetUserActivationData(int mode);
    }
}
