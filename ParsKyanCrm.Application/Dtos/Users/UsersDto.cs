using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestUsersDto : PageingParamerDto
    {

    }

    public class UsersDto
    {

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Status { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }
        
        public string Ip { get; set; }
    }
}
