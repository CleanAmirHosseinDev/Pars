using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{
    public class LoginLogDto
    {

        public int LoginLogID { get; set; }

        public int Userid { get; set; }

        public DateTime? LoginDate { get; set; }

        public string Ip { get; set; }

        public DateTime? SignOutDate { get; set; }

        public string AreaName { get; set; }
    }
}
