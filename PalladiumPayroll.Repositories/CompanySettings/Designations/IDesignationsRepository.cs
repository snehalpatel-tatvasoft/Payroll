using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.Comany_Settings;

public interface IDesignationsRepository
{
    Task<bool> CreateDesignations(DesignationRequestDTO request);
    Task<List<DesignationResponseDTO>> GetAllDesignations(long companyId);
    Task<bool> DeleteDesignations(long id);
    Task<bool> UpdateDesignations(DesignationRequestDTO request);
    Task<bool> CheckDuplicateDesignation(DesignationRequestDTO request);
    Task<string> ImportDesignations(ImportDesignationRequestDTO request);




}
