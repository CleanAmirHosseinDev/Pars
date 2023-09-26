using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetLevelStepSetting
{

    public class GetLevelStepSettingService : IGetLevelStepSettingService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetLevelStepSettingService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<LevelStepSettingDto> Execute(RequestLevelStepSettingDto request)
        {
            try
            {
                LevelStepSettingDto res = new LevelStepSettingDto();                

                if (request.LevelStepSettingIndexId != null && request.LevelStepSettingIndexId != 0)
                {
                    var q_Find = await _context.LevelStepSetting.FirstOrDefaultAsync(p => p.LevelStepSettingIndexId == request.LevelStepSettingIndexId);

                    res = _mapper.Map<LevelStepSettingDto>(q_Find);

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
