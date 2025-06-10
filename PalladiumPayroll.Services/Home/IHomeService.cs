using PalladiumPayroll.DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Services.Home
{
    public interface IHomeService
    {
        public List<Employee> GetAllEmployeeList();
    }
}
