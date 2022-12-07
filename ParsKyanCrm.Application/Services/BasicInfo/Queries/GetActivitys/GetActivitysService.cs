using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetActivitys
{

    public class GetActivitysService : IGetActivitysService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetActivitysService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<ActivityDto>>> Execute(RequestActivityDto request)
        {
            try
            {

                var lists = (from s in _context.Activity
                             select s).Include(p => p.ActivityTitleNavigation).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.ActivityTitleNavigation.Label.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "ActivityId_D":
                        lists = lists.OrderByDescending(s => s.ActivityId);
                        break;
                    case "ActivityId_A":
                        lists = lists.OrderBy(s => s.ActivityId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.ActivityId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<ActivityDto>>
                    {
                        Data = _mapper.Map<IEnumerable<ActivityDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var lists_Res_Pageing = (await Pagination<Activity>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<ActivityDto>>
                    {
                        Data = _mapper.Map<IEnumerable<ActivityDto>>(lists_Res_Pageing),
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
