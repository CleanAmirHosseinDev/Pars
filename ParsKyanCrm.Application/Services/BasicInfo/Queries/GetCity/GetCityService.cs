using AutoMapper;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetCity
{

    public class GetCityService : IGetCityService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetCityService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityDto> Execute(int? id = null)
        {
            try
            {

                CityDto res = new CityDto();

                if (id != null && id != 0)
                {
                    var q_Find = await _context.City.Include(p=>p.State).FirstOrDefaultAsync(p=>p.CityId == id.Value);
                    res = _mapper.Map<CityDto>(q_Find);
                    res.State = q_Find.State != null ? _mapper.Map<StateDto>(q_Find.State) : new StateDto();
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
