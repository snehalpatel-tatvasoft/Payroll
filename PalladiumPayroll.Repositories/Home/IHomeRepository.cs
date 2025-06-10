using Dapper;
using PalladiumPayroll.DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Repositories.Home
{
    public interface IHomeRepository
    {
        public List<T> ExecuteStoredProcedure<T>(string SPName, DynamicParameters DP);
    }
}
