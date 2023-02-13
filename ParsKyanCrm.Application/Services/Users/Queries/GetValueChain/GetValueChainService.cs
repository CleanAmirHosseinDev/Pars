using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Domain.Entities;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetValueChain
{

    public class GetValueChainService : IGetValueChainService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetValueChainService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

      

        public async Task<ResultDto<ValueChainDto>> Execute(RequestValueChainDto request)
        {
            try
            {
                ValueChainDto res = new ValueChainDto();                

                if (request.RequestId != null && request.RequestId != 0)
                {
                    var q_Find = await _context.ValueChain.FirstOrDefaultAsync(p => p.RequestId == request.RequestId);

                    res = _mapper.Map<ValueChainDto>(q_Find);

                    if (res == null)
                    {
                        return new ResultDto<ValueChainDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "اطلاعاتی پر نشده است",
                        };
                    }
                }

                return new ResultDto<ValueChainDto>
                {
                    Data = res,
                    IsSuccess = true,
                    Message = string.Empty,                    
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
