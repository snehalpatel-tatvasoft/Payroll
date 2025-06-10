using Dapper;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.Repositories.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Services.Home
{
    public class HomeService:IHomeService
    {
        public IHomeRepository _context;
        public HomeService(IHomeRepository _context) 
        {
            this._context = _context;
        }
        public List<Employee> GetAllEmployeeList()
        {
            DynamicParameters DP = new DynamicParameters();
            DP.Add("CompanyId", 1);

            return _context.ExecuteStoredProcedure<Employee>("SP_FetchEmployeeList",DP);
        }
    }
}
