using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;

namespace PalladiumPayroll.Services.Company
{
    public interface ICompanyService
    {
        Task<JsonResult> CreateCompany(CreateCompanyRequest request);
    }
}
