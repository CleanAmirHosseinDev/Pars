using AutoMapper;
using Dapper;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var sqlBuilder = new StringBuilder();
                var parameters = new DynamicParameters();

                sqlBuilder.AppendLine(@"
            SELECT ll.*,
                   CASE
                       WHEN us.CustomerID IS NOT NULL THEN
                           ISNULL(NULLIF(cus.AgentName, ''), N'ثبت نشده') +
                           N' - ' +
                           ISNULL(NULLIF(cus.CompanyName, ''), N'ثبت نشده')
                       ELSE
                           ISNULL(NULLIF(us.RealName, ''), ISNULL(us.UserName, N'ثبت نشده'))
                   END AS FullName,
                   COUNT(*) OVER() AS TotalRows
            FROM LoginLog AS ll
            LEFT JOIN Users AS us ON us.UserID = ll.UserID
            LEFT JOIN Customers AS cus ON cus.CustomerID = us.CustomerID
            WHERE 1 = 1
        ");

                parameters.Add("PageSize", request.PageSize);

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    sqlBuilder.AppendLine("AND (cus.AgentName LIKE @Search OR us.RealName LIKE @Search OR cus.CompanyName LIKE @Search OR us.UserName LIKE @Search)");
                    parameters.Add("Search", $"%{request.Search}%");
                }

                if (request.FromDateStr1.HasValue && request.ToDateStr1.HasValue)
                {
                    sqlBuilder.AppendLine("AND CAST(ll.LoginDate AS DATE) BETWEEN @FromDate AND @ToDate");
                    parameters.Add("FromDate", request.FromDateStr1.Value.Date);
                    parameters.Add("ToDate", request.ToDateStr1.Value.Date);
                }

                var orderByClause = BuildOrderByClause(request.SortOrder);
                sqlBuilder.AppendLine(orderByClause);

                sqlBuilder.AppendLine(@"
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
        ");
                parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);

                var result = await DapperOperation.Run<LoginLogDto>(sqlBuilder.ToString(), parameters);
                var totalRows = result.FirstOrDefault()?.TotalRows ?? 0;

                return new ResultDto<IEnumerable<LoginLogDto>>
                {
                    Data = result,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = totalRows,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string BuildOrderByClause(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
                return "ORDER BY ll.LoginLogID DESC"; 

            var parts = sortOrder.Split('_');
            if (parts.Length != 2)
                return "ORDER BY ll.LoginLogID DESC";

            string column = parts[0];
            string direction = parts[1] == "A" ? "ASC" : "DESC";

            string sqlColumn = column switch
            {
                "FullName" => "FullName",
                "LoginDate" => "ll.LoginDate",
                "SignOutDate" => "ll.SignOutDate",
                "Ip" => "ll.Ip",
                "AreaName" => "ll.AreaName",
                _ => "ll.LoginLogID"
            };

            return $"ORDER BY {sqlColumn} {direction}";
        }
    }
}