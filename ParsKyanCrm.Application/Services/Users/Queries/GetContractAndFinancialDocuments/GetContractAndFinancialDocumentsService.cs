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
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetContractAndFinancialDocumentsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        private string MaxAllContractCode()
        {
            try
            {
                List<ContractAndFinancialDocumentsDto> q = Ado_NetOperation.ConvertDataTableToList<ContractAndFinancialDocumentsDto>(Ado_NetOperation.GetAll_Table(typeof(ContractAndFinancialDocuments).Name, "cast(isnull((max(cast((isnull(ContractCode,'999')) as bigint))+1),1) as nvarchar(max)) as ContractCode"));
                if (q != null) return q.FirstOrDefault().ContractCode.ToString();
                return "1000";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Execute(RequestContractAndFinancialDocumentsDto request)
        {
            try
            {
                ContractAndFinancialDocumentsDto res = new ContractAndFinancialDocumentsDto();                

                if (request.RequestID != null && request.RequestID != 0)
                {
                    var q_Find = await _context.ContractAndFinancialDocuments.FirstOrDefaultAsync(p => p.RequestID == request.RequestID);

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

                    if (string.IsNullOrEmpty(q_Find.ContractCode))
                    {
                        Ado_NetOperation.SqlUpdate("ContractAndFinancialDocuments",new Dictionary<string, object>() {
                            {
                                "ContractCode",MaxAllContractCode()
                            }
                        }, string.Format(nameof(q_Find.FinancialId) + " = '{0}' ", q_Find.FinancialId));
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
