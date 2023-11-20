using AutoMapper;
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

                var q = await DapperOperation.Run<LoginLogDto>(@$"

                select top {request.PageSize} ll.*,iif(us.CustomerID is not null,isnull(cus.AgentName,N'ثبت نشده')+' - '+isnull(cus.CompanyName,N'ثبت نشده'),isnull(us.RealName,us.UserName)) as FullName from LoginLog as ll 
                left join Users as us on us.UserID = ll.Userid
                left join Customers as cus on cus.CustomerID = us.CustomerID
{(!string.IsNullOrEmpty(request.Search) ? " where (isnull(cus.AgentName,us.RealName)) like N'%" + request.Search + "%' " : string.Empty)}
{((!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr)) ? (!string.IsNullOrEmpty(request.Search) ? " and " : " where ") + " cast(ll.LoginDate as date) between " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}
order by ll.LoginLogID desc
");

                return new ResultDto<IEnumerable<LoginLogDto>>
                {
                    Data = q,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = q.LongCount(),
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
