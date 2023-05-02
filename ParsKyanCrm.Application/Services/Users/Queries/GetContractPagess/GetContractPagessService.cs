using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContract
{

    public class GetContractPagessService : IGetContractPagessService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetContractPagessService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<ContractPagesDto>>> Execute(RequestContractPagesDto request)
        {
            try
            {

                 var lists = (from s in _context.ContractPages
                             where (s.ContractId == request.ContractId)
                             select s).AsQueryable();

                //===========================================

                var res_Lists = await lists.ToListAsync();

                return new ResultDto<IEnumerable<ContractPagesDto>>
                {
                    Data = _mapper.Map<IEnumerable<ContractPagesDto>>(res_Lists),
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = res_Lists.LongCount(),
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
