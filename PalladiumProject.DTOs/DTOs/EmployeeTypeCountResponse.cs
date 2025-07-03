using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.DTOs.DTOs
{
    public class EmployeeTypeCountResponse
    {
        public int ActiveEmployee { get; set; }
        public int TerminatedEmployee { get; set; }
        public int ProcessedEmployee { get; set; }
        public int UnprocessedEmployee { get; set; }
        public int EmployeesOnLeave { get; set; }
    }
}
