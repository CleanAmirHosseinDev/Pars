using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersApprovedContract
{
    public class RequestTotalNumberCustomersApprovedContractDto : PageingParamerDto
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

    }
}
