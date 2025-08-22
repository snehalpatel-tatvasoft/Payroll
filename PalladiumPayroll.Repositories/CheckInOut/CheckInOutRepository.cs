using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.CheckInOut;

namespace PalladiumPayroll.Repositories.CheckInOut;

public class CheckInOutRepository : ICheckInOutRepository
{
    private readonly DapperContext _dapper;

    public CheckInOutRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<CheckInOutResultDTO> SaveClockInOut(CheckInOutRequesetDTO request)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@EmployeeCode", request.EmployeeCode);
        parameters.Add("@Password", request.Password);
        parameters.Add("@Date", request.Date.Date);
        parameters.Add("@IsCheckIn", request.IsCheckIn);

        CheckInOutResultDTO? result = await _dapper.ExecuteStoredProcedureSingle<CheckInOutResultDTO>("usp_SaveClockInOut", parameters);

        if (result == null)
        {
            return new CheckInOutResultDTO
            {
                IsSuccess = false,
                Message = "No response from stored procedure."
            };
        }
        
        return result;
    }

}
