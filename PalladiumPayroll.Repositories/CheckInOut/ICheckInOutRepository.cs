using PalladiumPayroll.DTOs.DTOs.CheckInOut;

namespace PalladiumPayroll.Repositories.CheckInOut;

public interface ICheckInOutRepository
{
    Task<CheckInOutResultDTO> SaveClockInOut(CheckInOutRequesetDTO request);
}
