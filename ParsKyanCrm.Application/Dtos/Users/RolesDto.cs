using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestRolesDto : PageingParamerDto
    {

    }
    public class RolesDto
    {

        public int RoleID { get; set; }

        public string RoleTitle { get; set; }
        
        public string RoleDesc { get; set; }

    }
}
