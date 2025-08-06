using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeePromotions;

namespace PalladiumPayroll.Services.HRFunctions.EmployeePromotions;

public interface IEmployeePromotionsService
{
    Task<JsonResult> UpsertEmployeePromotions(EmployeePromotionsUpsertData request);

    Task<JsonResult> DeleteEmployeePromotion(long employeePromotionId, string userId);

    Task<JsonResult> GetEmployeePromotionDropdownData(long companyId);

    Task<JsonResult> GetEmployeePromotions(long companyId);

    Task<JsonResult> GetEmployeePromotionById(long promotionId);
}
