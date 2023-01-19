using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveContractAndFinancialDocuments
{

    public class SaveContractAndFinancialDocumentsService : ISaveContractAndFinancialDocumentsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public SaveContractAndFinancialDocumentsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Execute(ContractAndFinancialDocumentsDto request)
        {
            try
            {
                #region Validation



                #endregion

                EntityEntry<ContractAndFinancialDocuments> q_Entity;
                if (request.FinancialId == 0)
                {
                    request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    request.Tax = Math.Round((request.PriceContract.HasValue ? request.PriceContract.Value * 9 : 0) / 100, 0);
                    q_Entity = _context.ContractAndFinancialDocuments.Add(_mapper.Map<ContractAndFinancialDocuments>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<ContractAndFinancialDocumentsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ContractAndFinancialDocuments).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.FinancialDocument),request.FinancialDocument
                        },
                        {
                            nameof(q_Entity.Entity.ContractDocument),request.ContractDocument
                        },
                        {
                            nameof(q_Entity.Entity.RequestID),request.RequestID
                        },
                        {
                            nameof(q_Entity.Entity.ContentContract),request.ContentContract
                        },
                        {
                            nameof(q_Entity.Entity.PriceContract),request.PriceContract
                        },
                        {
                            nameof(q_Entity.Entity.Tax),Math.Round((request.PriceContract.HasValue?request.PriceContract.Value * 9:0)/100,0)
                        },
                        {
                            nameof(q_Entity.Entity.SaveDate),DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
                        },
                    }, string.Format(nameof(q_Entity.Entity.FinancialId) + " = {0} ", request.FinancialId));
                }

                return new ResultDto<ContractAndFinancialDocumentsDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت نرخ نامه قرارداد با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
