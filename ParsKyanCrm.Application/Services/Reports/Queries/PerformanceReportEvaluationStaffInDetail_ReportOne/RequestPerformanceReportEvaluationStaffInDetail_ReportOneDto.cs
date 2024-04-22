using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;

namespace ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne
{
    public class RequestPerformanceReportEvaluationStaffInDetail_ReportOneDto : PageingParamerDto
    {
        public string FromDateStr { get; set; }
        public string FromDateStr1
        {
            get
            {
                if (!string.IsNullOrEmpty(FromDateStr))
                {
                    DateTime dtFromDate = DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(FromDateStr)));
                    return "'" + dtFromDate.Year +
                              "/" + dtFromDate.Month +
                              "/" + dtFromDate.Day + "'";
                }
                return "''";
            }

        }

        public string ToDateStr { get; set; }
        public string ToDateStr1
        {
            get
            {
                if (!string.IsNullOrEmpty(ToDateStr))
                {
                    DateTime dtFromDate = DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(ToDateStr)));
                    return "'" + dtFromDate.Year +
                              "/" + dtFromDate.Month +
                              "/" + dtFromDate.Day + "'";
                }
                return "''";
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string FromLastDateReferrals { get; set; }
        public string FromLastDateReferrals1
        {
            get
            {
                if (!string.IsNullOrEmpty(FromLastDateReferrals))
                {
                    DateTime dtFromDate = DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(FromLastDateReferrals)));
                    return "'" + dtFromDate.Year +
                              "/" + dtFromDate.Month +
                              "/" + dtFromDate.Day + "'";
                }
                return "''";
            }

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public string ToLastDateReferrals { get; set; }
        public string ToLastDateReferrals1
        {
            get
            {
                if (!string.IsNullOrEmpty(ToLastDateReferrals))
                {
                    DateTime dtFromDate = DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(ToLastDateReferrals)));
                    return "'" + dtFromDate.Year +
                              "/" + dtFromDate.Month +
                              "/" + dtFromDate.Day + "'";
                }
                return "''";
            }
        }

        public string ReciveUser { get; set; }

        public string cboSelectLS { get; set; }
        public bool IsExcel { get; set; }

    }
}
