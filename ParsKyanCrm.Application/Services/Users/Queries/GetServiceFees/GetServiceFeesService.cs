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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFees
{

    public class GetServiceFeesService : IGetServiceFeesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetServiceFeesService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<ServiceFeeDto>>> Execute(RequestServiceFeeDto request)
        {
            try
            {

                var lists = (from s in _context.ServiceFee
                             where (s.IsActive == request.IsActive || request.IsActive == null)
                             select s).Include(p => p.KindOfServiceNavigation).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p =>
                p.KindOfServiceNavigation.Label.Contains(request.Search)
                );

                switch (request.SortOrder)
                {
                    case "ServiceFeeId_D":
                        lists = lists.OrderByDescending(s => s.ServiceFeeId);
                        break;
                    case "ServiceFeeId_A":
                        lists = lists.OrderBy(s => s.ServiceFeeId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.ServiceFeeId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<ServiceFeeDto>>
                    {
                        Data = _mapper.Map<IEnumerable<ServiceFeeDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.ServiceFee>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<ServiceFeeDto>>
                    {
                        Data = _mapper.Map<IEnumerable<ServiceFeeDto>>(list_Res_Pageing),
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
