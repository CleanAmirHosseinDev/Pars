using AutoMapper;
using ClosedXML.Excel;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRequestForRatings
{
    public class GetRequestForRatingsService : IGetRequestForRatingsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetRequestForRatingsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<RequestForRatingDto>>> Execute(RequestRequestForRatingDto request)
        {
            try
            {
                // لیست شرایط WHERE برای فیلتر داینامیک
                List<string> conditions = new();

                // فیلتر تاریخ درخواست
                if (request.FromDate.HasValue && request.ToDate.HasValue)
                {
                    conditions.Add($"DATEADD(dd, 0, DATEDIFF(dd, 0, cteMain.DateOfRequest)) BETWEEN N'{request.FromDate.Value:yyyy-MM-dd}' AND N'{request.ToDate.Value:yyyy-MM-dd}'");
                }

                // فیلتر نوع گروه شرکت‌ها
                if (request.TypeGroupCompanies.HasValue)
                {
                    conditions.Add($"cteMain.TypeGroupCompanies = {request.TypeGroupCompanies.Value}");
                }

                // فیلتر بازه ارسال (SendTime)
                if (request.FromSendTimeDate.HasValue && request.ToSendTimeDate.HasValue)
                {
                    conditions.Add($"DATEADD(dd, 0, DATEDIFF(dd, 0, cteMain.SendTime)) BETWEEN N'{request.FromSendTimeDate.Value:yyyy-MM-dd}' AND N'{request.ToSendTimeDate.Value:yyyy-MM-dd}'");
                }

                // فیلتر ReciveUser
                if (request.ReciveUser.HasValue)
                {
                    conditions.Add($"cteMain.RequestID IN (SELECT DISTINCT RequestID FROM RequestReferences WHERE ReciveUser = N'{request.ReciveUser.Value}')");
                }

                // فیلتر CustomerId و RequestId که در زیر کوئری داخلی استفاده شده‌اند
                string innerWhere = "";
                if (request.CustomerId.HasValue)
                {
                    innerWhere += $" WHERE rfr.CustomerID = {request.CustomerId.Value}";
                }
                if (request.RequestId.HasValue)
                {
                    innerWhere += (string.IsNullOrEmpty(innerWhere) ? " WHERE " : " AND ") + $"rfr.RequestID = {request.RequestId.Value}";
                }

                var queryStr = @$"
select * from (

    select 
        cte.Assessment,
        cte.ReasonAssessment1,
        cte.ChangeDate,
        cte.RequestNo,
        cte.EvaluationExpert,
        cte.NationalCode,
        cte.TypeGroupCompanies,
        cte.AgentMobile,
        cte.AgentName,
        cte.CustomerID,
        cte.DateOfConfirmed,
        cte.DateOfRequest,
        cte.IsFinished,
        cte.KindOfRequest,
        cte.KindOfRequestName,
        cte.RequestID,
        cte.ContractDocument,
        (select distinct top 1 LevelStepAccessRole from LevelStepSetting where LevelStepIndex = (dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3))) as DestLevelStepAccessRole,
        dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',1) as LevelStepStatus,
        dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',2) as LevelStepAccessRole,
        dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) as DestLevelStepIndex,
        cte.CompanyName,
        (select top 1 RequestReferences.Comment from RequestReferences where RequestReferences.Requestid = cte.RequestID order by RequestReferences.ReferenceID desc) as Comment,
        dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',5) as DestLevelStepIndexButton,
        dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6) as ReciveUser,
        dbo.fn_GetAllNameUsers(dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6)) as ReciveUserName,
        dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',7) as SendUser,
        dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',8) as LevelStepSettingIndexID,
        dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',9) as SendTime,
        cte.CustomerRequestInformationId,
        cte.LastStatusChangeDate
    from (

        select 
            rfr.CustomerID,
            rfr.DateOfConfirmed,
            rfr.ChangeDate,
            rfr.DateOfRequest,
            rfr.IsFinished,
            rfr.KindOfRequest,
            rfr.RequestID,
            rfr.RequestNo,
            (select RealName from users where userid = (select top 1 ReciveUser from RequestReferences where ReciveUser is not null and [DestLevelStepIndex] = 6 and RequestID = rfr.RequestID)) as EvaluationExpert,
            (select top 1 
                RequestReferences.LevelStepStatus + '|' + 
                RequestReferences.LevelStepAccessRole + '|' + 
                RequestReferences.DestLevelStepIndex + '|' + 
                isnull(RequestReferences.Comment, N'') + '|' + 
                isnull(RequestReferences.DestLevelStepIndexButton, N'') + '|' + 
                isnull(RequestReferences.ReciveUser, '') + '|' + 
                isnull(CAST(RequestReferences.SendUser AS nvarchar), '0') + '|' + 
                isnull(CAST(RequestReferences.LevelStepSettingIndexID AS nvarchar), '0') + '|' + 
                CAST(RequestReferences.SendTime AS nvarchar) 
             from RequestReferences 
             where RequestReferences.Requestid = rfr.RequestID 
             order by RequestReferences.ReferenceID desc) as RequestReferences,
            (select CONVERT(nvarchar, max(SendTime), 120) from RequestReferences where RequestID = rfr.RequestID) as LastStatusChangeDate,
            ss.Label as KindOfRequestName,
            cus.AgentName,
            cus.AgentMobile,
            cus.CompanyName,
            cus.NationalCode,
            cus.TypeGroupCompanies,
            doc.ContractDocument,
            rfr.Assessment,
            rfr.ReasonAssessment1,
            cri.id as CustomerRequestInformationId
        from {typeof(RequestForRating).Name} as rfr
        left join {typeof(SystemSeting).Name} as ss on ss.SystemSetingID = rfr.KindOfRequest
        left join {typeof(Customers).Name} as cus on cus.CustomerID = rfr.CustomerID
        left join {typeof(ContractAndFinancialDocuments).Name} as doc on doc.RequestID = rfr.RequestID
        left join [dbo].[CustomerRequestInformation] as cri on rfr.RequestID = cri.RequestId
        {innerWhere}
    ) as cte

) as cteMain

{(conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : string.Empty)}

{(!string.IsNullOrEmpty(request.DestLevelStepIndex?.ToString()) ? (conditions.Count > 0 ? " AND " : " WHERE ") + $"cteMain.DestLevelStepIndex = {request.DestLevelStepIndex}" : string.Empty)}

{(!string.IsNullOrEmpty(request.Search) ? (conditions.Count > 0 || !string.IsNullOrEmpty(request.DestLevelStepIndex?.ToString()) ? " AND " : " WHERE ") + $"(cteMain.CompanyName LIKE N'%{request.Search}%' OR cteMain.AgentMobile LIKE N'%{request.Search}%')" : string.Empty)}

{(!string.IsNullOrEmpty(request.LoginName) && request.IsMyRequests ? ((conditions.Count > 0 || !string.IsNullOrEmpty(request.DestLevelStepIndex?.ToString()) || !string.IsNullOrEmpty(request.Search)) ? " AND " : " WHERE ") + $"cteMain.LevelStepAccessRole = N'{request.LoginName}'" : string.Empty)}

{(request.IsMyRequests ? " AND ((cteMain.ReciveUser = N'" + request.UserID + "' OR cteMain.ReciveUser = N''))" : string.Empty)}

{(request.KindOfRequest.HasValue ? ((conditions.Count > 0 || !string.IsNullOrEmpty(request.DestLevelStepIndex?.ToString()) || !string.IsNullOrEmpty(request.Search) || !string.IsNullOrEmpty(request.LoginName)) ? " AND " : " WHERE ") + $"cteMain.KindOfRequest = {request.KindOfRequest.Value}" : string.Empty)}

{(!string.IsNullOrEmpty(request.UserID) ? ((conditions.Count > 0 || !string.IsNullOrEmpty(request.DestLevelStepIndex?.ToString()) || !string.IsNullOrEmpty(request.Search) || request.KindOfRequest.HasValue) ? " AND " : " WHERE ") + $"cteMain.ReciveUser LIKE N'%{request.UserID}%'" : string.Empty)}

order by cteMain.ChangeDate desc

OFFSET {(request.PageIndex <= 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT {request.PageSize} ROWS ONLY
";

                var data = await DapperOperation.Run<RequestForRatingDto>(queryStr);

                if (request.IsCorporate == 1)
                {
                    data = data.Where(p => p.KindOfRequest == 254);
                }
                else if (request.IsCorporate == 2)
                {
                    data = data.Where(p => p.KindOfRequest == 66);
                }

                request.PageSize = (request.IsExcelReport == true ? data.Count() : request.PageSize);

                return new ResultDto<IEnumerable<RequestForRatingDto>>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = data.LongCount(),
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<byte[]> Execute1(RequestRequestForRatingDto request)
        {
            try
            {
                request.IsExcel = true;
                var q = await Execute(request);

                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("ردیف"),
                new DataColumn("شماره درخواست"),
                new DataColumn("تاریخ ثبت درخواست "),
                new DataColumn("نام شرکت"),
                new DataColumn("نام رابط"),
                new DataColumn("شناسه/کد ملی"),
                new DataColumn("موبایل رابط	"),
                new DataColumn("تاریخ کدال	"),
                new DataColumn("کد رهگیری	")
            });
                int rowcount = 1;
                foreach (var item in q.Data)
                {
                    dt.Rows.Add(
                          rowcount,
                          item.RequestNo,
                          item.DateOfRequestStr,
                          item.CompanyName,
                          item.AgentName,
                          item.NationalCode,
                          item.AgentMobile,
                          item.CodalDate,
                          item.CodalNumber

                        );
                    rowcount++;
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream.ToArray();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto<IEnumerable<RequestForRatingDto>>> ExecuteHistory(RequestRequestForRatingDto request)
        {
            try
            {

                var data = await DapperOperation.Run<RequestForRatingDto>(@$"
                                        select top 1 *  from RequestForRating where CustomerID in( select CustomerID from RequestForRating where RequestID={request.RequestId}) 
                                        and KindOfRequest=(select KindOfRequest from RequestForRating where RequestID={request.RequestId}) and IsFinished=1 and (select count(*) from DataFormAnswerTables where RequestId={request.RequestId})=0 order by RequestID desc
                  ");

               // request.PageSize = (request.IsExcelReport == true ? data.Count() : request.PageSize);

                return new ResultDto<IEnumerable<RequestForRatingDto>>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = data.LongCount(),
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
