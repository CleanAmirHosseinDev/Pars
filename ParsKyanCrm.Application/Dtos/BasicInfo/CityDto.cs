using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{
    public class RequestCityDto : PageingParamerDto
    {
        public int? StateID { get; set; }
    }

    public class CityDto
    {

        public int CityID { get; set; }

        public int? StateID { get; set; }

        public string CityName { get; set; }

        public StateDto State { get; set; }
    }
}
