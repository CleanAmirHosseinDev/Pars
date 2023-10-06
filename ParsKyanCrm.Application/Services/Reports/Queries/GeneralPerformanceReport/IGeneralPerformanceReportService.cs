using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.GeneralPerformanceReport
{
    public interface IGeneralPerformanceReportService
    {
        Task<byte[]> Execute(RequestRequestForRatingDto request);
    }
}
