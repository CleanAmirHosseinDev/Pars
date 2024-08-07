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
        public double? SystemScore { get; set; }
        public double? AnalizeScore { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }
        public string ManagerReport { get; set; }
    }
}
