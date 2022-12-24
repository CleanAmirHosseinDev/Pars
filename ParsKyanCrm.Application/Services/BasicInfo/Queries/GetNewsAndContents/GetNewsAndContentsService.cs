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

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetNewsAndContents
{

    public class GetNewsAndContentsService : IGetNewsAndContentsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetNewsAndContentsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<NewsAndContentDto>>> Execute(RequestNewsAndContentDto request)
        {
            try
            {

                var lists = (from s in _context.NewsAndContent
                             where (s.IsActive == request.IsActive || request.IsActive == null)
                             select s).Include(p=>p.User).Include(p=>p.KindOfContentNavigation).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Title.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "ContentId_D":
                        lists = lists.OrderByDescending(s => s.ContentId);
                        break;
                    case "ContentId_A":
                        lists = lists.OrderBy(s => s.ContentId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.ContentId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<NewsAndContentDto>>
                    {
                        Data = _mapper.Map<IEnumerable<NewsAndContentDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var lists_Res_Pageing = (await Pagination<NewsAndContent>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<NewsAndContentDto>>
                    {
                        Data = _mapper.Map<IEnumerable<NewsAndContentDto>>(lists_Res_Pageing),
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
