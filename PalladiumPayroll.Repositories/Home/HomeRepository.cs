using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PalladiumPayroll.Repositories.Home
{
    public class HomeRepository:IHomeRepository
    {
        private readonly DapperContext _context;

        public HomeRepository(DapperContext context)
        {
            _context = context;
        }
        public List<T> ExecuteStoredProcedure<T>(string SPName, DynamicParameters DP)
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<T>(SPName,DP).ToList();
            }
        }
    }
}
