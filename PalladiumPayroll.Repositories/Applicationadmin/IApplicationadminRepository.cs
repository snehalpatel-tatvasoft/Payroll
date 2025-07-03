using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.Applicationadmin
{
    public interface IApplicationadminRepository
    {
        Task<List<UserActivation>> GetUserActivationData(int mode);
    }
}
