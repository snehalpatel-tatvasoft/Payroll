using Dapper;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.Applicationadmin
{
    public class ApplicationadminRepository : IApplicationadminRepository
    {
        private readonly DapperContext _dapper;
        public ApplicationadminRepository(DapperContext _dapper)
        {
            this._dapper = _dapper;
        }

        public async Task<List<UserActivation>> GetUserActivationData(int mode)
        {
            List<UserActivation> userActivations = new List<UserActivation>();

            var parameters = new DynamicParameters();
            parameters.Add("@mode", mode);
            var data = await _dapper.ExecuteStoredProcedure<UserActivation>("SP_GetUserActivationData", parameters);

            if (data.Count != 0)
            {
                userActivations = data;
            }
            return userActivations;
        }
    }
}
