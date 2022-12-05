using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetManagerOfParsKyan
{

    public class GetManagerOfParsKyanService : IGetManagerOfParsKyanService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetManagerOfParsKyanService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ManagerOfParsKyanDto> Execute(RequestManagerOfParsKyanDto request)
        {
            try
            {

                ManagerOfParsKyanDto res = new ManagerOfParsKyanDto();
                res.User = new UsersDto();
                res.Position = new SystemSetingDto();
                res.Titel = new SystemSetingDto();

                if (request.ManagersId != null && request.ManagersId != 0)
                {
                    var q_Find = await _context.ManagerOfParsKyan.Include(p => p.User).Include(p => p.Position).Include(p => p.Titel).FirstOrDefaultAsync(p => p.ManagersId == request.ManagersId.Value);
                    res = _mapper.Map<ManagerOfParsKyanDto>(q_Find);
                    res.User = q_Find.User != null ? _mapper.Map<UsersDto>(q_Find.User) : new UsersDto();
                    res.Position = q_Find.Position != null ? _mapper.Map<SystemSetingDto>(q_Find.Position) : new SystemSetingDto();
                    res.Titel = q_Find.Titel != null ? _mapper.Map<SystemSetingDto>(q_Find.Titel) : new SystemSetingDto();
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
