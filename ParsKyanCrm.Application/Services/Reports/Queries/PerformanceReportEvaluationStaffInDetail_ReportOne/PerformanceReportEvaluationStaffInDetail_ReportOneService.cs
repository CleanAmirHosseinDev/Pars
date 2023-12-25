using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne
{

    public class PerformanceReportEvaluationStaffInDetail_ReportOneService : IPerformanceReportEvaluationStaffInDetail_ReportOneService
    {
        public async Task<ResultDto<IEnumerable<ResultPerformanceReportEvaluationStaffInDetail_ReportOneDto>>> Execute(RequestPerformanceReportEvaluationStaffInDetail_ReportOneDto request)
        {
            try
            {

                string strQuery = @$"


select ROW_NUMBER() OVER (order by rfr.RequestID desc) as [Row],cte.Requestid,rfr.DateOfRequest,format(rfr.DateOfRequest,'yyyy/MM/dd','fa') as DateOfRequestStr,cus.CompanyName,
(select top 1 format(RequestReferences.SendTime,'yyyy/MM/dd','fa') from RequestReferences where RequestReferences.Requestid = cte.Requestid order by RequestReferences.ReferenceID desc)
as LastDateReferrals,
(select top 1 RequestReferences.LevelStepStatus from RequestReferences where RequestReferences.Requestid = cte.Requestid order by RequestReferences.ReferenceID desc) as LastSituation,
'' as WaitingTimeInThisSituation
from (
	
		select Requestid from RequestReferences as rr
		{(!string.IsNullOrEmpty(request.ReciveUser) && request.ReciveUser !="0" ? " where rr.ReciveUser = " + request.ReciveUser : string.Empty)}
		group by Requestid

) as cte
left join RequestForRating as rfr on rfr.RequestID = cte.Requestid
left join Customers as cus on cus.CustomerID = rfr.CustomerID

{(!string.IsNullOrEmpty(request.Search) ? " where ( cus.CompanyName like N'%" + request.Search + "%'" + " or cus.AgentName like N'%" + request.Search + "%' or rfr.RequestNo like N'%" + request.Search + "%' or cus.NationalCode like N'%" + request.Search + "%' or cus.AgentMobile like N'%" + request.Search + "%' )" : string.Empty)}        
{(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? (!string.IsNullOrEmpty(request.Search) ? " and " : " where ") + " cast(rfr.DateOfRequest as date) between  " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}               

order by rfr.RequestID desc              

";
                strQuery += @$" OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT { request.PageSize} ROWS ONLY";

                var data = await DapperOperation.Run<ResultPerformanceReportEvaluationStaffInDetail_ReportOneDto>(strQuery);

                return new ResultDto<IEnumerable<ResultPerformanceReportEvaluationStaffInDetail_ReportOneDto>>()
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
