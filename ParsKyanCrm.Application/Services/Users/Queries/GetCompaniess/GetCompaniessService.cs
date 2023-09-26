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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCompaniess
{

    public class GetCompaniessService : IGetCompaniessService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetCompaniessService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<IEnumerable<CompaniesDto>>> Execute(RequestCompaniesDto request)
        {
            try
            {

                var lists = (from s in _context.Companies
                             where (s.IsActive == request.IsActive || request.IsActive == null)
                             select s).Include(p => p.CompanyGroup).Include(p => p.KindOfCompanyNavigation).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p =>
                p.CompanyName.Contains(request.Search)
                );

                switch (request.SortOrder)
                {
                    case "CompaniesId_D":
                        lists = lists.OrderByDescending(s => s.CompaniesId);
                        break;
                    case "CompaniesId_A":
                        lists = lists.OrderBy(s => s.CompaniesId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.CompaniesId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<CompaniesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<CompaniesDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.Companies>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<CompaniesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<CompaniesDto>>(list_Res_Pageing),
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
