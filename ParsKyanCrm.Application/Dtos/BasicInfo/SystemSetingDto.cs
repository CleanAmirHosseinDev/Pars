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
        public int? SystemSetingId { get; set; }

        public int? LabeCode { get; set; }
    }

    public class SystemSetingDto : BaseEntityDto
    {

        public int SystemSetingId { get; set; }
        public string Label { get; set; }
        public int? LabeCode { get; set; }
        public string Value { get; set; }        
        public int? BaseAmount { get; set; }
        public string TiTleBaseAmount { get; set; }
        public double? FromAmount { get; set; }
        public double? ToAmount { get; set; }
        public int? ChangeBy { get; set; }
        public DateTime? ChangeDate { get; set; }

    }
}
