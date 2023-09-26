using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetActivity
{

    public class GetActivityService : IGetActivityService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetActivityService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActivityDto> Execute(RequestActivityDto request)
        {
            try
            {

                ActivityDto res = new ActivityDto();

                if (request.ActivityId != null && request.ActivityId != 0)
                {
                    var q_Find = await _context.Activity.Include(p => p.ActivityTitleNavigation).FirstOrDefaultAsync(p => p.ActivityId == request.ActivityId.Value && (p.IsActive == request.IsActive || request.IsActive == null));
                    res = _mapper.Map<ActivityDto>(q_Find);
                    res.ActivityTitleNavigation = q_Find.ActivityTitleNavigation != null ? _mapper.Map<SystemSetingDto>(q_Find.ActivityTitleNavigation) : new SystemSetingDto();
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
