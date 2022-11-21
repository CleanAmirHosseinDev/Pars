using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveSystemSeting
{

    public class SaveSystemSetingService : ISaveSystemSetingService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveSystemSetingService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<SystemSetingDto>> Execute(SystemSetingDto request)
        {
            try
            {
                EntityEntry<SystemSeting> q_Entity;
                if (request.SystemSetingId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    q_Entity = _context.SystemSeting.Add(_mapper.Map<SystemSeting>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<SystemSetingDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(SystemSeting).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.LabelCode),request.LabeCode
                        },
                        {
                            nameof(q_Entity.Entity.Label),request.Label
                        },
                        {
                            nameof(q_Entity.Entity.Value),request.Value
                        },
                        {
                            nameof(q_Entity.Entity.FromAmount),request.FromAmount
                        },
                        {
                            nameof(q_Entity.Entity.ToAmount),request.ToAmount
                        },
                        {
                            nameof(q_Entity.Entity.BaseAmount),request.BaseAmount
                        }
                    }, string.Format(nameof(q_Entity.Entity.SystemSetingId) + " = {0} ", request.SystemSetingId));
                }

                return new ResultDto<SystemSetingDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت تنظیمات سیستم با موفقیت انجام شد",
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
