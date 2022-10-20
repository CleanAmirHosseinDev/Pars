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

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveState
{

    public class SaveStateService : ISaveStateService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveStateService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<StateDto>> Execute(StateDto request)
        {
            try
            {
                EntityEntry<State> q_Entity;
                if (request.StateId == 0)
                {
                    q_Entity = _context.State.Add(_mapper.Map<State>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<StateDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(State).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.StateName),request.StateName
                        }
                    }, string.Format(nameof(q_Entity.Entity.StateId) + " = {0} ", request.StateId));
                }

                return new ResultDto<StateDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت استان با موفقیت انجام شد",
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
