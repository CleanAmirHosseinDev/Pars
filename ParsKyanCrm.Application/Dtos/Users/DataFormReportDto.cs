using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestDataFormReportDto : PageingParamerDto
    {
        public int? DataReportId { get; set; }
        public int? RequestId { get; set; }
        public int? DataFormAnswerId { get; set; }
    }

    public class DataFormReportDto
    {
        public int DataReportId { get; set; }
        public int RequestId { get; set; }
        public int DataFormAnswerId { get; set; }
        public int SystemScore { get; set; }
        public int AnalizeScore { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }

    }
}
