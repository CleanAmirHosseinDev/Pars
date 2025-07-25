using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.StalledRequestsReport
{
    public interface IStalledRequestsReportService
    {
        Task<StalledRequestsReportDto> Execute(RequestStalledRequestsReportDto request);
    }
}
