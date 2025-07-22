
using AutoMapper;
using Dapper;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetLoginLogs
{

    public class GetLoginLogsService : IGetLoginLogsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetLoginLogsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<LoginLogDto>>> Execute(RequestLoginLogDto request)
        {
            try
            {
                var parameters = new DynamicParameters();
                var baseQuery = @"
                                  WITH FilteredLogs AS (
                                      SELECT 
                                          ll.LoginLogID,
                                          ll.UserID,
                                          ll.LoginDate,
                                          ll.SignOutDate,
                                          ll.Ip,
                                          ll.AreaName,
                                          CASE 
                                              WHEN us.CustomerID IS NOT NULL THEN
                                                  COALESCE(NULLIF(cus.AgentName, ''), N'ثبت نشده') 
                                              ELSE
                                                  COALESCE(NULLIF(us.RealName, ''), us.UserName, N'ثبت نشده')
                                          END AS NamePart1,
                                          CASE 
                                              WHEN us.CustomerID IS NOT NULL THEN
                                                  COALESCE(NULLIF(cus.CompanyName, ''), N'ثبت نشده')
                                              ELSE NULL
                                          END AS NamePart2
                                      FROM LoginLog AS ll WITH (NOLOCK)
                                      LEFT JOIN Users AS us WITH (NOLOCK) ON us.UserID = ll.UserID
                                      LEFT JOIN Customers AS cus WITH (NOLOCK) ON cus.CustomerID = us.CustomerID
                                      WHERE 1 = 1
                                  ";

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    baseQuery += @"
                                    AND (
                                        (us.CustomerID IS NOT NULL AND (cus.AgentName LIKE @Search OR cus.CompanyName LIKE @Search))
                                        OR
                                        (us.CustomerID IS NULL AND (us.RealName LIKE @Search OR us.UserName LIKE @Search))
                                    )";
                    parameters.Add("Search", $"%{request.Search}%");
                }

                if (request.FromDateStr1.HasValue && request.ToDateStr1.HasValue)
                {
                    baseQuery += @"
                                    AND ll.LoginDate >= @FromDate AND ll.LoginDate < DATEADD(day, 1, @ToDate)";
                    parameters.Add("FromDate", request.FromDateStr1.Value.Date);
                    parameters.Add("ToDate", request.ToDateStr1.Value.Date);
                }

                var countQuery = baseQuery + @")
                                                SELECT COUNT(*) FROM FilteredLogs";

                var orderByClause = BuildOrderByClause(request.SortOrder);
                var pagedQuery = baseQuery + @")
                                               SELECT 
                                                   LoginLogID,
                                                   UserID,
                                                   LoginDate,
                                                   SignOutDate,
                                                   Ip,
                                                   AreaName,
                                                   CASE 
                                                       WHEN NamePart2 IS NOT NULL THEN NamePart1 + N' - ' + NamePart2
                                                       ELSE NamePart1
                                                   END AS FullName
                                               FROM FilteredLogs
                                               " + orderByClause + @"
                                               OFFSET @Offset ROWS
                                               FETCH NEXT @PageSize ROWS ONLY";

                parameters.Add("PageSize", request.PageSize);
                parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);

                var dataTask = DapperOperation.Run<LoginLogDto>(pagedQuery, parameters);
                var countTask = DapperOperation.RunScalar<long>(countQuery, parameters);

                await Task.WhenAll(dataTask, countTask);

                return new ResultDto<IEnumerable<LoginLogDto>>
                {
                    Data = await dataTask,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = await countTask
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string BuildOrderByClause(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
                return "ORDER BY LoginLogID DESC";

            var parts = sortOrder.Split(' ');
            if (parts.Length != 2)
                return "ORDER BY LoginLogID DESC";

            string column = parts[0];
            string direction = parts[1].ToLower() == "asc" ? "ASC" : "DESC";

            string sqlColumn = column switch
            {
                "LoginDate" => "LoginDate",
                "SignOutDate" => "SignOutDate",
                "Ip" => "Ip",
                "AreaName" => "AreaName",
                "FullName" => "NamePart1",
                _ => "LoginLogID"
            };

            return $"ORDER BY {sqlColumn} {direction}";
        }
    }
}