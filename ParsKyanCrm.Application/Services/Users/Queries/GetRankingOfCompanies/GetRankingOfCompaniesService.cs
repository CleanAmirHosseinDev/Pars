using AutoMapper;
using Microsoft.EntityFrameworkCore;

using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRankingOfCompanies
{

    public class GetRankingOfCompaniesService : IGetRankingOfCompaniesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetRankingOfCompaniesService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RankingOfCompaniesDto> Execute(RequestRankingOfCompaniesDto request)
        {
            try
            {

                RankingOfCompaniesDto res = new RankingOfCompaniesDto();
                res.User = new UsersDto();
                res.Comany = new CompaniesDto();

                if (request.RankingId != null && request.RankingId != 0)
                {
                    var q_Find = await _context.RankingOfCompanies.Include(p => p.User).Include(p => p.Comany).FirstOrDefaultAsync(p => p.RankingId == request.RankingId.Value && (p.IsActive == request.IsActive || request.IsActive == null));
                    res = _mapper.Map<RankingOfCompaniesDto>(q_Find);
                    res.User = q_Find.User != null ? _mapper.Map<UsersDto>(q_Find.User) : new UsersDto();
                    res.Comany = q_Find.Comany != null ? _mapper.Map<CompaniesDto>(q_Find.Comany) : new CompaniesDto();
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
