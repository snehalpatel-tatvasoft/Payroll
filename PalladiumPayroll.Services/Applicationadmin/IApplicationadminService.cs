using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Services.Applicationadmin
{
    public interface IApplicationadminService
    {
        Task<List<UserActivation>> GetUserActivationData(int mode);
    }
}
