using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetSystemSetings
{

    public class GetSystemSetingsService : IGetSystemSetingsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetSystemSetingsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<SystemSetingDto>>> Execute(RequestSystemSetingDto request)
        {
            try
            {

                var lists = (from s in _context.SystemSeting
                             where (s.IsActive == request.IsActive || request.IsActive == null)
                             select s);

                if (request.ParentCode.HasValue) lists = lists.Where(p => p.ParentCode == request.ParentCode.Value); 

                if (!string.IsNullOrEmpty(request.ParentCodeArr))
                {
                    string[] qSplit = request.ParentCodeArr.Split(",");
                    lists = lists.Where(p => qSplit.Contains(p.ParentCode.ToString()));
                }

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Label.Contains(request.Search) || p.Value.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "SystemSetingId_D":
                        lists = lists.OrderByDescending(s => s.SystemSetingId);
                        break;
                    case "SystemSetingId_A":
                        lists = lists.OrderBy(s => s.SystemSetingId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.SystemSetingId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<SystemSetingDto>>
                    {
                        Data = _mapper.Map<IEnumerable<SystemSetingDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var lists_Res_Pageing = (await Pagination<SystemSeting>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<SystemSetingDto>>
                    {
                        Data = _mapper.Map<IEnumerable<SystemSetingDto>>(lists_Res_Pageing),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = lists_Res_Pageing.Rows,
                    };

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
