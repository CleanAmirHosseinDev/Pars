using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne2
{
    public interface IPerformanceReportEvaluationStaffInDetail_ReportOne2Service
    {
        Task<ResultDto<IEnumerable<ResultPerformanceReportEvaluationStaffInDetail_ReportOne2Dto>>> Execute(RequestPerformanceReportEvaluationStaffInDetail_ReportOne2Dto request);
    }
}
