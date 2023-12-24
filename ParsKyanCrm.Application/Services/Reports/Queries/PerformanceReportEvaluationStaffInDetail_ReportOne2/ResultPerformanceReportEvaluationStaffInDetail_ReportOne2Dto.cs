using System;

namespace ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne2
{
    public class ResultPerformanceReportEvaluationStaffInDetail_ReportOne2Dto
    {

        public string ReciveUser { get; set; }

        public long Row { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }

        public int NumberCompletedRequests { get; set; }

        public int NumberOpenAndCurrentRequests { get; set; }

        public string AverageResponseTimeRequestsStageSendingAdditionalInformationCustomer { get; set; }

    }
}
