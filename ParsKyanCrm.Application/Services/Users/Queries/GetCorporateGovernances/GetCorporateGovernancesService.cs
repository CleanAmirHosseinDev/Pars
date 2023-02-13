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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCorporateGovernances
{

    public class GetCorporateGovernancesService : IGetCorporateGovernancesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetCorporateGovernancesService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

      

        public async Task<ResultDto<CorporateGovernanceDto>> Execute(RequestCorporateGovernanceDto request)
        {
            try
            {
                CorporateGovernanceDto res = new CorporateGovernanceDto();                

                if (request.RequestId != null && request.RequestId != 0)
                {
                    var q_Find = await _context.CorporateGovernance.FirstOrDefaultAsync(p => p.RequestId == request.RequestId);

                    res = _mapper.Map<CorporateGovernanceDto>(q_Find);

                    if (res == null)
                    {
                        return new ResultDto<CorporateGovernanceDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "اطلاعاتی پر نشده است",
                        };
                    }
                }

                return new ResultDto<CorporateGovernanceDto>
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
