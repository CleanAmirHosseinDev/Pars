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

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetManagerOfParsKyans
{

    public class GetManagerOfParsKyansService : IGetManagerOfParsKyansService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetManagerOfParsKyansService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<ManagerOfParsKyanDto>>> Execute(RequestManagerOfParsKyanDto request)
        {
            try
            {

                var lists = (from s in _context.ManagerOfParsKyan
                             where (s.IsActive == request.IsActive || request.IsActive == null)
                             select s).Include(p => p.User).Include(p=>p.Title).Include(p=>p.Position).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.NameOfManager.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "ManagersId_D":
                        lists = lists.OrderByDescending(s => s.ManagersId);
                        break;
                    case "ManagersId_A":
                        lists = lists.OrderBy(s => s.ManagersId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.ManagersId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<ManagerOfParsKyanDto>>
                    {
                        Data = _mapper.Map<IEnumerable<ManagerOfParsKyanDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var lists_Res_Pageing = (await Pagination<ManagerOfParsKyan>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<ManagerOfParsKyanDto>>
                    {
                        Data = _mapper.Map<IEnumerable<ManagerOfParsKyanDto>>(lists_Res_Pageing),
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
