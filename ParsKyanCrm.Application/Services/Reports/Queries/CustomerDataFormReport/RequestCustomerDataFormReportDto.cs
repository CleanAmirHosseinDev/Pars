using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;

namespace ParsKyanCrm.Application.Services.Reports.Queries.CustomerDataFormReport
{
    public class RequestCustomerDataFormReportDto : PageingParamerDto
    {
        public int? RequestId { get; set; }
    }
}
