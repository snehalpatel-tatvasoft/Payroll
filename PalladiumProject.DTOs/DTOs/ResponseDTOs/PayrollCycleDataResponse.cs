using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs
{
    public class PayrollCycleDataResponse
    {
        public int PayrollSetupId { get; set; }
        public string? PayrollCycleName { get; set; }
        public string? CycleEndDate { get; set; }
        public int NoOfEmployees { get; set; }
        public bool IsSelected { get; set; }
    }
}
