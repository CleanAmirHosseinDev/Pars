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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRankingOfCompaniess
{

    public class GetRankingOfCompaniessService : IGetRankingOfCompaniessService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetRankingOfCompaniessService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<RankingOfCompaniesDto>>> Execute(RequestRankingOfCompaniesDto request)
        {
            try
            {

                var lists = (from s in _context.RankingOfCompanies
                             where (s.IsActive == request.IsActive || request.IsActive == null)
                             select s).Include(p => p.User).Include(p => p.Comany).Include(p=>p.Comany.CompanyGroup).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Comany.CompanyName.Contains(request.Search));
                /*
                for(int i =0 ;i<lists.Count() ;i++) {
                    if(lists.ElementAt(i).Comany.CompanyGroupId != null) {
                        int x = Convert.ToInt32(lists.ElementAt(i).Comany.CompanyGroupId + "");
                        lists.ElementAt(i).Comany.CompanyGroup = _context.SystemSeting.Where(a=>a.SystemSetingId == x).First();
                    }
                    
                }*/
                switch (request.SortOrder)
                {
                    case "RankingId_D":
                        lists = lists.OrderByDescending(s => s.RankingId);
                        break;
                    case "RankingId_A":
                        lists = lists.OrderBy(s => s.RankingId);
                        break;

                    case "CompanyName_A":
                        lists = lists.OrderBy(s => s.Comany.CompanyName);
                        break;
                    case "CompanyName_D":
                        lists = lists.OrderByDescending(s => s.Comany.CompanyName);
                        break;

                    case "PublishDate_A":
                        lists = lists.OrderBy(s => s.PublishDate);
                        break;
                    case "PublishDate_D":
                        lists = lists.OrderByDescending(s => s.PublishDate);
                        break;

                    case "RankingTypeText_A":
                        lists = lists.OrderBy(s => s.RankingTypeText);
                        break;
                    case "RankingTypeText_D":
                        lists = lists.OrderByDescending(s => s.RankingTypeText);
                        break;

                    case "StatusText_A":
                        lists = lists.OrderBy(s => s.StatusText);
                        break;
                    case "StatusText_D":
                        lists = lists.OrderByDescending(s => s.StatusText);
                        break;

                    case "LongTermRating_A":
                        lists = lists.OrderBy(s => s.LongTermRating);
                        break;
                    case "LongTermRating_D":
                        lists = lists.OrderByDescending(s => s.LongTermRating);
                        break;

                    case "ShortTermRating_A":
                        lists = lists.OrderBy(s => s.ShortTermRating);
                        break;
                    case "ShortTermRating_D":
                        lists = lists.OrderByDescending(s => s.ShortTermRating);
                        break;

                    case "Vision_A":
                        lists = lists.OrderBy(s => s.Vision);
                        break;
                    case "Vision_D":
                        lists = lists.OrderByDescending(s => s.Vision);
                        break;

                    case "TradingSymbol_A":
                        lists = lists.OrderBy(s => s.TradingSymbol);
                        break;
                    case "TradingSymbol_D":
                        lists = lists.OrderByDescending(s => s.TradingSymbol);
                        break;

                    default:
                        lists = lists.OrderByDescending(s => s.RankingId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<RankingOfCompaniesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<RankingOfCompaniesDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var lists_Res_Pageing = (await Pagination<RankingOfCompanies>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<RankingOfCompaniesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<RankingOfCompaniesDto>>(lists_Res_Pageing),
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
