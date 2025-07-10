using PalladiumPayroll.DTOs.DTOs.Common;

namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.Company
{
    public class GLConnRes
    {
        public bool IsDbConnection {  get; set; }
        public string ConnectionMessage {  get; set; }
        public List<DropDownViewModelWithString> GlAccountList { get; set; }
        public List<DropDownViewModelWithString> GlDepartmentList { get; set; }
    }
}
