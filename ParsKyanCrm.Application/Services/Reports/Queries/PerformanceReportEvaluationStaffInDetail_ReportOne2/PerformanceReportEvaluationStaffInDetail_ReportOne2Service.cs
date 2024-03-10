using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne2
{

    public class PerformanceReportEvaluationStaffInDetail_ReportOne2Service : IPerformanceReportEvaluationStaffInDetail_ReportOne2Service
    {
        public async Task<ResultDto<IEnumerable<ResultPerformanceReportEvaluationStaffInDetail_ReportOne2Dto>>> Execute(RequestPerformanceReportEvaluationStaffInDetail_ReportOne2Dto request)
        {
            try
            {

                string strQuery = @$"





select ROW_NUMBER() OVER (order by r.RoleDesc desc) as [Row],cte.ReciveUser,ISNULL(u.RealName,u.UserName) as UserName,r.RoleDesc as RoleName,
(
	select count(distinct RequestReferences.Requestid) from RequestReferences
	inner join RequestForRating on RequestForRating.RequestID = RequestReferences.Requestid
	where RequestReferences.ReciveUser = cte.ReciveUser and RequestForRating.IsFinished = 1

{(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " and cast(CodalDate as date) between  " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}               
) as NumberCompletedRequests,
(
	select count(distinct RequestReferences.Requestid) from RequestReferences
	inner join RequestForRating on RequestForRating.RequestID = RequestReferences.Requestid
	where RequestReferences.ReciveUser = cte.ReciveUser and RequestForRating.IsFinished = 0
{(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " and cast(SendTime as date) between  " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}               

) as NumberOpenAndCurrentRequests,

'' as AverageResponseTimeRequestsStageSendingAdditionalInformationCustomer

from(

	select rr.ReciveUser from RequestReferences as rr
	where rr.ReciveUser is not null
group by rr.ReciveUser
	
) as cte
left join Users as u on u.UserID = cte.ReciveUser
left join UserRoles as ur on ur.UserID = u.UserID
left join Roles as r on r.RoleID = ur.RoleID

order by r.RoleDesc desc              

";
                strQuery += @$" OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT { request.PageSize} ROWS ONLY";

                var data = await DapperOperation.Run<ResultPerformanceReportEvaluationStaffInDetail_ReportOne2Dto>(strQuery);

                return new ResultDto<IEnumerable<ResultPerformanceReportEvaluationStaffInDetail_ReportOne2Dto>>()
                {
                    Data = data,
                    Rows = data.Count(),
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
