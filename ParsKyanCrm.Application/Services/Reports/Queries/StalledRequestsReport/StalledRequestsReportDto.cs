using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;

namespace ParsKyanCrm.Application.Services.Reports.Queries.StalledRequestsReport
{
    public class RequestStalledRequestsReportDto : PageingParamerDto
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
        public StalledRequestCategory? Category { get; set; }
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
        public int? RequestNo { get; set; }
        public string DateOfRequestStr { get; set; }
        public string DateOfRequestStr1
        {
            get
            {
                if (!string.IsNullOrEmpty(DateOfRequestStr))
                {
                    DateTime dtFromDate = DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(DateOfRequestStr)));
                    return "'" + dtFromDate.Year +
                              "/" + dtFromDate.Month +
                              "/" + dtFromDate.Day + "'";
                }
                return "''";
            }
        }
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
