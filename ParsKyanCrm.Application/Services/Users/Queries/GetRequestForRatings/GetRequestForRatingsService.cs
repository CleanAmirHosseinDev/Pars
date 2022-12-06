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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRequestForRatings
{

    public class GetRequestForRatingsService : IGetRequestForRatingsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetRequestForRatingsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<RequestForRatingDto>>> Execute(RequestRequestForRatingDto request)
        {
            try
            {

                var lists = (from s in _context.RequestForRating
                             where (s.CustomerId == request.CustomerId || request.CustomerId == null)
                             select s).Include(p => p.KindOfRequestNavigation).Include(p => p.Customer).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.RequestNo.ToString().Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "RequestId_D":
                        lists = lists.OrderByDescending(s => s.RequestId);
                        break;
                    case "RequestId_A":
                        lists = lists.OrderBy(s => s.RequestId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.RequestId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<RequestForRatingDto>>
                    {
                        Data = _mapper.Map<IEnumerable<RequestForRatingDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.RequestForRating>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<RequestForRatingDto>>
                    {
                        Data = _mapper.Map<IEnumerable<RequestForRatingDto>>(list_Res_Pageing),
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
