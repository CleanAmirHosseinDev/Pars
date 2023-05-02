using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContract
{

    public class GetContractPagesService : IGetContractPagesService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetContractPagesService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ContractPagesDto> Execute(RequestContractPagesDto request)
        {
            try
            {
                ContractPagesDto res = new ContractPagesDto();
                
                if (request.ContractPageId != null && request.ContractPageId != 0)
                {
                    var q_Find = await _context.ContractPages.FirstOrDefaultAsync(p => p.ContractPageId == request.ContractPageId.Value );
                    res = _mapper.Map<ContractPagesDto>(q_Find);                   
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
