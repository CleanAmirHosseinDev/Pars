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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCustomerss
{

    public class GetCustomerssService : IGetCustomerssService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetCustomerssService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<CustomersDto>>> Execute(RequestCustomersDto request)
        {
            try
            {

                var lists = (from s in _context.Customers
                             where (s.IsActive == request.IsActive || request.IsActive == null)
                             select s).Include(p => p.City).Include(p => p.HowGetKnowCompany).Include(p => p.KindOfCompany).Include(p => p.TypeServiceRequested).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.AgentName.Contains(request.Search)
                || p.AgentMobile.Contains(request.Search)
                );

                switch (request.SortOrder)
                {
                    case "CustomerId_D":
                        lists = lists.OrderByDescending(s => s.CustomerId);
                        break;
                    case "CustomerId_A":
                        lists = lists.OrderBy(s => s.CustomerId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.CustomerId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<CustomersDto>>
                    {
                        Data = _mapper.Map<IEnumerable<CustomersDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.Customers>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<CustomersDto>>
                    {
                        Data = _mapper.Map<IEnumerable<CustomersDto>>(list_Res_Pageing),
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
