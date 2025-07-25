﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

    public class GetContractService : IGetContractService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetContractService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ContractDto> Execute(RequestContractDto request)
        {
            try
            {
                ContractDto res = new ContractDto();
                res.KinfOfRequestNavigation = new SystemSetingDto();

                if (request.ContractId != null && request.ContractId != 0)
                {
                    var q_Find = await _context.Contract.FirstOrDefaultAsync(p => p.ContractId == request.ContractId.Value && (p.IsActive == request.IsActive || request.IsActive == null));

                    res = _mapper.Map<ContractDto>(q_Find);
                    res.KinfOfRequestNavigation = q_Find.KinfOfRequestNavigation != null ? _mapper.Map<SystemSetingDto>(q_Find.KinfOfRequestNavigation) : new SystemSetingDto();

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
