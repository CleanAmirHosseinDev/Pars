using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetSystemSeting
{

    public class GetSystemSetingService : IGetSystemSetingService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetSystemSetingService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SystemSetingDto> Execute(RequestSystemSetingDto request)
        {
            try
            {
                SystemSetingDto res = new SystemSetingDto();

                if (request.SystemSetingId != null && request.SystemSetingId != 0)
                {

                    var q_Find = await _context.SystemSeting.FirstOrDefaultAsync(p => p.SystemSetingId == request.SystemSetingId.Value && (p.IsActive == request.IsActive || request.IsActive == null));
                    if (q_Find.ParentCode != null)
                    {
                        var q_parent = await _context.SystemSeting.FirstOrDefaultAsync(p => p.SystemSetingId == q_Find.ParentCode);
                        res.ParenLabel = q_parent.Label;
                    }
                   
                    res = _mapper.Map<SystemSetingDto>(q_Find);

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
