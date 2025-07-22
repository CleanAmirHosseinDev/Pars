

using AutoMapper;
using ClosedXML.Excel;
using Dapper;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
                List<string> conditions = new();

                if (request.FromDate.HasValue && request.ToDate.HasValue)
                {
                    conditions.Add($"DATEADD(dd, 0, DATEDIFF(dd, 0, cteMain.DateOfRequest)) BETWEEN N'{request.FromDate.Value:yyyy-MM-dd}' AND N'{request.ToDate.Value:yyyy-MM-dd}'");
                }

                if (request.TypeGroupCompanies.HasValue)
                {
                    conditions.Add($"cteMain.TypeGroupCompanies = {request.TypeGroupCompanies.Value}");
                }

                if (request.FromSendTimeDate.HasValue && request.ToSendTimeDate.HasValue)
                {
                    conditions.Add($"DATEADD(dd, 0, DATEDIFF(dd, 0, cteMain.SendTime)) BETWEEN N'{request.FromSendTimeDate.Value:yyyy-MM-dd}' AND N'{request.ToSendTimeDate.Value:yyyy-MM-dd}'");
                }

                if (request.ReciveUser.HasValue)
                {
                    conditions.Add($"cteMain.RequestID IN (SELECT DISTINCT RequestID FROM RequestReferences WHERE ReciveUser = N'{request.ReciveUser.Value}')");
                }

                string innerWhere = "";
                if (request.CustomerId.HasValue)
                {
                    innerWhere += $" WHERE rfr.CustomerID = {request.CustomerId.Value}";
                }
                if (request.RequestId.HasValue)
                {
                    innerWhere += (string.IsNullOrEmpty(innerWhere) ? " WHERE " : " AND ") + $"rfr.RequestID = {request.RequestId.Value}";
                }

                var baseQuery = @$"
                                   SELECT * FROM (
                                       SELECT 
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
                                           (SELECT DISTINCT TOP 1 LevelStepAccessRole FROM LevelStepSetting WHERE LevelStepIndex = (dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3))) AS DestLevelStepAccessRole,
                                           dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',1) AS LevelStepStatus,
                                           dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',2) AS LevelStepAccessRole,
                                           dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) AS DestLevelStepIndex,
                                           cte.CompanyName,
                                           (SELECT TOP 1 RequestReferences.Comment FROM RequestReferences WHERE RequestReferences.Requestid = cte.RequestID ORDER BY RequestReferences.ReferenceID DESC) AS Comment,
                                           dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',5) AS DestLevelStepIndexButton,
                                           dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6) AS ReciveUser,
                                           dbo.fn_GetAllNameUsers(dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6)) AS ReciveUserName,
                                           dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',7) AS SendUser,
                                           dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',8) AS LevelStepSettingIndexID,
                                           dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',9) AS SendTime,
                                           cte.CustomerRequestInformationId,
                                           cte.LastStatusChangeDate
                                       FROM (
                                           SELECT 
                                               rfr.CustomerID,
                                               rfr.DateOfConfirmed,
                                               rfr.ChangeDate,
                                               rfr.DateOfRequest,
                                               rfr.IsFinished,
                                               rfr.KindOfRequest,
                                               rfr.RequestID,
                                               rfr.RequestNo,
                                               (SELECT RealName FROM users WHERE userid = (SELECT TOP 1 ReciveUser FROM RequestReferences WHERE ReciveUser IS NOT NULL AND [DestLevelStepIndex] = 6 AND RequestID = rfr.RequestID)) AS EvaluationExpert,
                                               (SELECT TOP 1 
                                                   RequestReferences.LevelStepStatus + '|' + 
                                                   RequestReferences.LevelStepAccessRole + '|' + 
                                                   RequestReferences.DestLevelStepIndex + '|' + 
                                                   ISNULL(RequestReferences.Comment, N'') + '|' + 
                                                   ISNULL(RequestReferences.DestLevelStepIndexButton, N'') + '|' + 
                                                   ISNULL(RequestReferences.ReciveUser, '') + '|' + 
                                                   ISNULL(CAST(RequestReferences.SendUser AS nvarchar), '0') + '|' + 
                                                   ISNULL(CAST(RequestReferences.LevelStepSettingIndexID AS nvarchar), '0') + '|' + 
                                                   CAST(RequestReferences.SendTime AS nvarchar) 
                                                FROM RequestReferences 
                                                WHERE RequestReferences.Requestid = rfr.RequestID 
                                                ORDER BY RequestReferences.ReferenceID DESC) AS RequestReferences,
                                               (SELECT CONVERT(nvarchar, MAX(SendTime), 120) FROM RequestReferences WHERE RequestID = rfr.RequestID) AS LastStatusChangeDate,
                                               ss.Label AS KindOfRequestName,
                                               cus.AgentName,
                                               cus.AgentMobile,
                                               cus.CompanyName,
                                               cus.NationalCode,
                                               cus.TypeGroupCompanies,
                                               doc.ContractDocument,
                                               rfr.Assessment,
                                               rfr.ReasonAssessment1,
                                               cri.id AS CustomerRequestInformationId
                                           FROM RequestForRating rfr
                                           LEFT JOIN SystemSeting ss ON ss.SystemSetingID = rfr.KindOfRequest
                                           LEFT JOIN Customers cus ON cus.CustomerID = rfr.CustomerID
                                           LEFT JOIN ContractAndFinancialDocuments doc ON doc.RequestID = rfr.RequestID
                                           LEFT JOIN CustomerRequestInformation cri ON rfr.RequestID = cri.RequestId
                                           {innerWhere}
                                       ) AS cte
                                   ) AS cteMain";
                string fullWhere = "";
                if (conditions.Count > 0)
                    fullWhere += "WHERE " + string.Join(" AND ", conditions);

                if (!string.IsNullOrEmpty(request.DestLevelStepIndex?.ToString()))
                    fullWhere += (string.IsNullOrEmpty(fullWhere) ? "WHERE " : " AND ") + $"cteMain.DestLevelStepIndex = {request.DestLevelStepIndex}";

                if (!string.IsNullOrEmpty(request.Search))
                    fullWhere += (string.IsNullOrEmpty(fullWhere) ? "WHERE " : " AND ") + $"(cteMain.CompanyName LIKE N'%{request.Search}%' OR cteMain.AgentMobile LIKE N'%{request.Search}%')";

                if (!string.IsNullOrEmpty(request.LoginName) && request.IsMyRequests)
                    fullWhere += (string.IsNullOrEmpty(fullWhere) ? "WHERE " : " AND ") + $"cteMain.LevelStepAccessRole = N'{request.LoginName}'";

                if (request.IsMyRequests)
                    fullWhere += $" AND ((cteMain.ReciveUser = N'{request.UserID}' OR cteMain.ReciveUser = N''))";

                if (request.KindOfRequest.HasValue)
                    fullWhere += (string.IsNullOrEmpty(fullWhere) ? "WHERE " : " AND ") + $"cteMain.KindOfRequest = {request.KindOfRequest.Value}";

                if (!string.IsNullOrEmpty(request.UserID))
                    fullWhere += (string.IsNullOrEmpty(fullWhere) ? "WHERE " : " AND ") + $"cteMain.ReciveUser LIKE N'%{request.UserID}%'";

                var parameters = new DynamicParameters();
                var countQuery = $"SELECT COUNT(*) FROM ({baseQuery} {fullWhere}) AS TotalTable";
                var totalCount = await DapperOperation.RunScalar<long>(countQuery, parameters);

                long maxPageIndex = (long)Math.Ceiling((double)totalCount / request.PageSize);
                if (request.PageIndex > maxPageIndex && maxPageIndex > 0)
                    request.PageIndex = (int)maxPageIndex;
                else if (maxPageIndex == 0)
                    request.PageIndex = 1;

                var allowedSortColumns = new List<string>
                {
                    "ChangeDate", "CompanyName", "AgentName", "RequestNo", "KindOfRequest", "DateOfRequest"
                };

                string sortColumn = !string.IsNullOrEmpty(request.SortColumn) && allowedSortColumns.Contains(request.SortColumn)
                    ? request.SortColumn
                    : "ChangeDate";

                string sortDirection = request.SortDirection?.ToUpper() == "ASC" ? "ASC" : "DESC";

                var pagingClause = $@"
                                      ORDER BY cteMain.{sortColumn} {sortDirection}
                                      OFFSET {(request.PageIndex <= 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
                                      FETCH NEXT {request.PageSize} ROWS ONLY";

                var finalQuery = $@"{baseQuery} {fullWhere} {pagingClause}";

                var data = await DapperOperation.Run<RequestForRatingDto>(finalQuery);

                if (request.IsCorporate == 1)
                    data = data.Where(p => p.KindOfRequest == 254);
                else if (request.IsCorporate == 2)
                    data = data.Where(p => p.KindOfRequest == 66);

                request.PageSize = request.IsExcelReport == true ? data.Count() : request.PageSize;

                return new ResultDto<IEnumerable<RequestForRatingDto>>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = totalCount
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
                new DataColumn("تاریخ ثبت درخواست"),
                new DataColumn("نام شرکت"),
                new DataColumn("نام رابط"),
                new DataColumn("شناسه/کد ملی"),
                new DataColumn("موبایل رابط"),
                new DataColumn("تاریخ کدال"),
                new DataColumn("کد رهگیری")
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