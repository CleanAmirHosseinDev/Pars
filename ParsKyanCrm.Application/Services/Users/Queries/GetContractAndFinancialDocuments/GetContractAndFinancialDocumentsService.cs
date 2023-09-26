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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContractAndFinancialDocuments
{

    public class GetContractAndFinancialDocumentsService : IGetContractAndFinancialDocumentsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetContractAndFinancialDocumentsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }        

        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Execute(RequestContractAndFinancialDocumentsDto request)
        {
            try
            {
                ContractAndFinancialDocumentsDto res = new ContractAndFinancialDocumentsDto();                

                if (request.RequestID != null && request.RequestID != 0)
                {
                    var q_Find = await _context.ContractAndFinancialDocuments.FirstOrDefaultAsync(p => p.RequestID == request.RequestID && p.IsActive==15);

                    res = _mapper.Map<ContractAndFinancialDocumentsDto>(q_Find);

                    if (res == null)
                    {
                        return new ResultDto<ContractAndFinancialDocumentsDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "قراردادی تنظیم نشده است",
                        };
                    }

                }

                return new ResultDto<ContractAndFinancialDocumentsDto>
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
