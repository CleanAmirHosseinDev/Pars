using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne
{
    public interface IPerformanceReportEvaluationStaffInDetail_ReportOneService
    {
        Task<ResultDto<IEnumerable<ResultPerformanceReportEvaluationStaffInDetail_ReportOneDto>>> Execute(RequestPerformanceReportEvaluationStaffInDetail_ReportOneDto request);
        Task<byte[]> Execute1(RequestPerformanceReportEvaluationStaffInDetail_ReportOneDto request);
    }
}
