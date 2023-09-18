using AutoMapper;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetLoginLogs
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

                select top {request.PageSize} ll.*,(isnull(cus.AgentName,us.RealName)) as FullName from LoginLog as ll 
                left join Users as us on us.UserID = ll.Userid
                left join Customers as cus on cus.CustomerID = ll.Userid
{(!string.IsNullOrEmpty(request.Search) ? " where (isnull(cus.AgentName,us.RealName)) like N'%" + request.Search + "%' " : string.Empty)}
{((!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr)) ? (!string.IsNullOrEmpty(request.Search) ? " and " : " where ") + " ll.LoginDate between " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}
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
