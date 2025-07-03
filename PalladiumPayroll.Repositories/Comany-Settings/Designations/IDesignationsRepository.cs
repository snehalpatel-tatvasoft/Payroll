using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.Comany_Settings;

public interface IDesignationsRepository
{
    Task<bool> CreateDesignations(DesignationRequestDto request);
    Task<List<DesignationResponseDto>> GetAllDesignations(long companyId);
    Task<bool> DeleteDesignations(long id);
    Task<bool> UpdateDesignations(DesignationRequestDto request);
    Task<bool> CheckDuplicateDesignation(DesignationRequestDto request);
    Task<string> ImportDesignations(ImportDesignationRequestDto request);




}
