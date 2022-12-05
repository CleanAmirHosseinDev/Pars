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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContracts
{

    public class GetContractsService : IGetContractsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetContractsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<ContractDto>>> Execute(RequestContractDto request)
        {
            try
            {

                var lists = (from s in _context.Contract
                             select s).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p =>
                p.ContractText.Contains(request.Search)
                );

                switch (request.SortOrder)
                {
                    case "ContractId_D":
                        lists = lists.OrderByDescending(s => s.ContractId);
                        break;
                    case "ContractId_A":
                        lists = lists.OrderBy(s => s.ContractId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.ContractId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<ContractDto>>
                    {
                        Data = _mapper.Map<IEnumerable<ContractDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.Contract>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<ContractDto>>
                    {
                        Data = _mapper.Map<IEnumerable<ContractDto>>(list_Res_Pageing),
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
