using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Securitys.Queries.Logins
{
    public class ResultLoginDto
    {

        public string Token { get; set; }

        public string CustomerID { get; set; }

        public int UserID { get; set; }

        public List<NormalJsonClassDto> Menus { get; set; }

        public string FullName { get; set; }


    }
}
