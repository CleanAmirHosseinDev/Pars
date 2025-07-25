using System.Collections.Generic;

namespace ParsKyanCrm.Application.Services.Reports.Queries.StalledRequestsReport
{
    public class RequestStalledRequestsReportDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string FromDateStr { get; set; }
        public string ToDateStr { get; set; }
        public StalledRequestCategory Category { get; set; }
    }

    public class StalledRequestsReportDto
    {
        public List<StalledRequestDto> Data { get; set; }
        public int Rows { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StalledRequestDto
    {
        public string CompanyName { get; set; }
        public string RequestNo { get; set; }
        public string DateOfRequestStr { get; set; }
        public string Status { get; set; }
        public int DelayInDays { get; set; }
    }

    public enum StalledRequestCategory
    {
        UnconfirmedContracts,
        IncompleteInfo,
        StalledWithAssessor,
        StalledInEvaluationCommittee
    }
}
