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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveServiceFee
{

    public class SaveServiceFeeService : ISaveServiceFeeService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public SaveServiceFeeService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<ServiceFeeDto>> Execute(ServiceFeeDto request)
        {
            try
            {
                #region Validation

                

                #endregion

                EntityEntry<ServiceFee> q_Entity;
                if (request.ServiceFeeId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    request.ChangeDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.ServiceFee.Add(_mapper.Map<ServiceFee>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<ServiceFeeDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Contract).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.KindOfService),request.KindOfService
                        },
                        {
                            nameof(q_Entity.Entity.FromCompanyRange),request.FromCompanyRange
                        },
                        {
                            nameof(q_Entity.Entity.ToCompanyRange),request.ToCompanyRange
                        },
                        {
                            nameof(q_Entity.Entity.FixedCost),request.FixedCost
                        },
                        {
                            nameof(q_Entity.Entity.VariableCost),request.VariableCost
                        },
                        {
                            nameof(q_Entity.Entity.ChangeBy),request.ChangeBy
                        },
                        {
                            nameof(q_Entity.Entity.ChangeDate),DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
                        },
                    }, string.Format(nameof(q_Entity.Entity.ServiceFeeId) + " = {0} ", request.ServiceFeeId));
                }

                return new ResultDto<ServiceFeeDto>()
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
