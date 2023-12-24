using System;

namespace ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne
{
    public class ResultPerformanceReportEvaluationStaffInDetail_ReportOneDto
    {

        public long Row { get; set; }

        public int? Requestid { get; set; }

        public DateTime? DateOfRequest { get; set; }

        public string DateOfRequestStr { get; set; }

        public string CompanyName { get; set; }

        /// <summary>
        /// آخرین تاریخ ارجاعات
        /// </summary>
        public string LastDateReferrals { get; set; }

        /// <summary>
        /// آخرین وضعیت
        /// </summary>
        public string LastSituation { get; set; }


        /// <summary>
        /// مدت زمان انتظار در این وضعیت
        /// </summary>
        public string WaitingTimeInThisSituation { get; set; }

    }
}
