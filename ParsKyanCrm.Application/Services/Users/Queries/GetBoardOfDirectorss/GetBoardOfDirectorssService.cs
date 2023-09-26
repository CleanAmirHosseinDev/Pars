using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetBoardOfDirectorss
{

    public class GetBoardOfDirectorssService : IGetBoardOfDirectorssService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetBoardOfDirectorssService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<IEnumerable<BoardOfDirectorsDto>>> Execute(RequestBoardOfDirectorsDto request)
        {
            try
            {

                var lists = (from s in _context.BoardOfDirectors
                             where (s.IsActive == request.IsActive || request.IsActive == null) &&
                             (s.CustomerId == request.CustomerId || request.CustomerId == null)
                             select s).Include(p => p.University).Include(p => p.MemberPost).Include(p => p.MemberEduction).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.MemberName.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "BoardOfDirectorsId_D":
                        lists = lists.OrderByDescending(s => s.BoardOfDirectorsId);
                        break;
                    case "BoardOfDirectorsId_A":
                        lists = lists.OrderBy(s => s.BoardOfDirectorsId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.BoardOfDirectorsId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<BoardOfDirectorsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<BoardOfDirectorsDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.BoardOfDirectors>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<BoardOfDirectorsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<BoardOfDirectorsDto>>(list_Res_Pageing),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = list_Res_Pageing.Rows,
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
