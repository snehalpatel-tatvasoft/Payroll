namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

public class DesignationResponseDTO
{
        public int Id { get; set; }
        public string DesignationsName { get; set; } = null! ;
        public string DesignationsCode { get; set; } = null!;
        public int CompanyId { get; set; }
}
