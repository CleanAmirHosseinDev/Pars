using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetSystemSeting
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

                if (request.SystemSetingID != null && request.SystemSetingID != 0)
                {

                    var q_Find = await _context.SystemSetings.FirstOrDefaultAsync(p => p.SystemSetingID == request.SystemSetingID.Value && (p.IsActive == request.IsActive || request.IsActive == null));

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
