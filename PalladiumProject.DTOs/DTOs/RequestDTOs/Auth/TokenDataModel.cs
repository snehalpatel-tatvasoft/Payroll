using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth
{
    public class TokenDataModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
