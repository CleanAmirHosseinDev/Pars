using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestUserRolesDto : PageingParamerDto
    {
        public int? UserID { get; set; }
    }

    public class UserRolesDto
    {

        public int Id { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public string Roles { get; set; }

        public UsersDto User { get; set; }

        public RolesDto Role { get; set; }

    }
}
