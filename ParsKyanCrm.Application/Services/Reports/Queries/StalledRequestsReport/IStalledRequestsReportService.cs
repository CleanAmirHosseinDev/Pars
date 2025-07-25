using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.StalledRequestsReport
{
    public interface IStalledRequestsReportService
    {
        Task<ResultDto<IEnumerable<StalledRequestDto>>> Execute(RequestStalledRequestsReportDto request);
    }
}
