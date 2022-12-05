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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveContract
{

    public class SaveContractService : ISaveContractService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public SaveContractService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<ContractDto>> Execute(ContractDto request)
        {
            try
            {
                #region Validation



                #endregion

                EntityEntry<Contract> q_Entity;
                if (request.ContractId == 0)
                {
                    q_Entity = _context.Contract.Add(_mapper.Map<Contract>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<ContractDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Contract).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.ContractText),request.ContractText
                        },
                        {
                            nameof(q_Entity.Entity.KinfOfRequest),request.KinfOfRequest
                        },
                    }, string.Format(nameof(q_Entity.Entity.ContractId) + " = {0} ", request.ContractId));
                }

                return new ResultDto<ContractDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت قرارداد و اصلاحیه قرارداد با موفقیت انجام شد",
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
