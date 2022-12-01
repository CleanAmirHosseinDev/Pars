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

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetStates
{

    public class GetStatesService : IGetStatesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetStatesService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<StateDto>>> Execute(RequestStateDto request)
        {
            try
            {

                var lists = (from s in _context.State
                              select s);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.StateName.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "StateId_D":
                        lists = lists.OrderByDescending(s => s.StateId);
                        break;
                    case "StateId_A":
                        lists = lists.OrderBy(s => s.StateId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.StateId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<StateDto>>
                    {
                        Data = _mapper.Map<IEnumerable<StateDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var lists_Res_Pageing = (await Pagination<State>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<StateDto>>
                    {
                        Data = _mapper.Map<IEnumerable<StateDto>>(lists_Res_Pageing),
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
