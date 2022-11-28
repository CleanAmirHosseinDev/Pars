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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetEducationCoursess
{

    public class GetEducationCoursessService : IGetEducationCoursessService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetEducationCoursessService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<EducationCoursesDto>>> Execute(RequestEducationCoursesDto request)
        {
            try
            {

                var lists = (from s in _context.EducationCourses
                             where (s.IsActive == request.IsActive || request.IsActive == null) &&
                             (s.CustomerId == request.CustomerId || request.CustomerId == null) &&
                             (s.BoardOfDirectorsId == request.BoardOfDirectorsId || request.BoardOfDirectorsId == null)
                             select s).Include(p => p.Customer).Include(p => p.BoardOfDirectors).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.TitelCourses == request.Search);

                switch (request.SortOrder)
                {
                    case "EducationCoursesId_D":
                        lists = lists.OrderByDescending(s => s.EducationCoursesId);
                        break;
                    case "EducationCoursesId_A":
                        lists = lists.OrderBy(s => s.EducationCoursesId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.EducationCoursesId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<EducationCoursesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<EducationCoursesDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.EducationCourses>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<EducationCoursesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<EducationCoursesDto>>(list_Res_Pageing),
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
