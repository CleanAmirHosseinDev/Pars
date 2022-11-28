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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetWorkExperiences
{

    public class GetWorkExperiencesService : IGetWorkExperiencesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetWorkExperiencesService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<WorkExperienceDto>>> Execute(RequestWorkExperienceDto request)
        {
            try
            {

                var lists = (from s in _context.WorkExperience
                             where (s.IsActive == request.IsActive || request.IsActive == null) &&
                             (s.CustomerId == request.CustomerId || request.CustomerId == null) &&
                             (s.BoardOfDirectorsId == request.BoardOfDirectorsId || request.BoardOfDirectorsId == null)
                             select s).Include(p => p.Customer).Include(p => p.BoardOfDirectors).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.InsuranceHistory.ToString() == request.Search);

                switch (request.SortOrder)
                {
                    case "SkilsId_D":
                        lists = lists.OrderByDescending(s => s.SkilsId);
                        break;
                    case "SkilsId_A":
                        lists = lists.OrderBy(s => s.SkilsId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.SkilsId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<WorkExperienceDto>>
                    {
                        Data = _mapper.Map<IEnumerable<WorkExperienceDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.WorkExperience>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<WorkExperienceDto>>
                    {
                        Data = _mapper.Map<IEnumerable<WorkExperienceDto>>(list_Res_Pageing),
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
