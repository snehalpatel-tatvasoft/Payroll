using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeePromotions;

namespace PalladiumPayroll.Repositories.HRFunctions.EmployeePromotions;

public interface IEmployeePromotionsRepository
{
    Task<bool> UpsertEmployeePromotions(EmployeePromotionsUpsertData request);

    Task<bool> DeleteEmployeePromotion(long employeePromotionId, string userId);

    Task<EmployeePromotionDropdownsDTO> GetEmployeePromotionDropdownData(long companyId);

    Task<List<EmployeePromotionsdisplayDataDTO>> GetEmployeePromotionsDisplayData(long companyId);

    Task<EmployeePromotionDetailDTO?> GetEmployeePromotioneById(long promotionId);
}
