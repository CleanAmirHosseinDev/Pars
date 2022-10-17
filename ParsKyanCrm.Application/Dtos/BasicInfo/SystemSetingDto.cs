using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{
    public class RequestSystemSetingDto : PageingParamerDto
    {
        public int? SystemSetingID { get; set; }

        public int? LabeCode { get; set; }
    }

    public class SystemSetingDto : BaseEntityDto
    {

        public int SystemSetingID { get; set; }

        public string Label { get; set; }

        public int? LabeCode { get; set; }
        
        public string Value { get; set; }

        public int? BaseAmount { get; set; }

        public double? FromAmount { get; set; }

        public double? ToAmount { get; set; }
    }
}
