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

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetRankingOfCompaniess
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
