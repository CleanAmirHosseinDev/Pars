using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities.BasicInfo;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveCity
{

    public class SaveCityService : ISaveCityService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveCityService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<CityDto>> Execute(CityDto request)
        {
            try
            {
                EntityEntry<City> q_Entity;
                if (request.CityID == 0)
                {
                    q_Entity = _context.City.Add(_mapper.Map<City>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<CityDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(City).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.CityName),request.CityName
                        },
                        {
                            nameof(q_Entity.Entity.StateID),request.StateID
                        }
                    }, string.Format(nameof(q_Entity.Entity.CityID) + " = {0} ", request.CityID));
                }

                return new ResultDto<CityDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت شهر با موفقیت انجام شد",
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
