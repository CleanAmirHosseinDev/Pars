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

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetCitys
{

    public class GetCitysService : IGetCitysService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetCitysService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<CityDto>>> Execute(RequestCityDto request)
        {
            try
            {

                var lists = (from s in _context.City
                             where (s.StateId == request.StateId || request.StateId == null)
                             select s).Include(p => p.State).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.CityName.Contains(request.Search) || p.State.StateName.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "CityID_D":
                        lists = lists.OrderByDescending(s => s.CityId);
                        break;
                    case "CityID_A":
                        lists = lists.OrderBy(s => s.CityId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.CityId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<CityDto>>
                    {
                        Data = _mapper.Map<IEnumerable<CityDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var lists_Res_Pageing = (await Pagination<City>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<CityDto>>
                    {
                        Data = _mapper.Map<IEnumerable<CityDto>>(lists_Res_Pageing),
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
