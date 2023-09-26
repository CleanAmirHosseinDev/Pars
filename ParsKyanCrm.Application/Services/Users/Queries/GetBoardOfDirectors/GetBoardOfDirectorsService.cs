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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetBoardOfDirectors
{

    public class GetBoardOfDirectorsService : IGetBoardOfDirectorsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetBoardOfDirectorsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<BoardOfDirectorsDto> Execute(RequestBoardOfDirectorsDto request)
        {
            try
            {
                BoardOfDirectorsDto res = new BoardOfDirectorsDto();
                res.University = new Dtos.Users.SystemSetingDto();
                res.MemberPost = new Dtos.Users.SystemSetingDto();
                res.MemberEduction = new Dtos.Users.SystemSetingDto();

                if (request.BoardOfDirectorsId != null && request.BoardOfDirectorsId != 0)
                {
                    var q_Find = await _context.BoardOfDirectors.Include(p => p.University).Include(p => p.MemberPost).Include(p => p.MemberEduction).FirstOrDefaultAsync(p => p.BoardOfDirectorsId == request.BoardOfDirectorsId.Value && (p.IsActive == request.IsActive || request.IsActive == null) && (p.CustomerId == request.CustomerId || request.CustomerId == null));

                    res = _mapper.Map<BoardOfDirectorsDto>(q_Find);
                    res.University = q_Find.University != null ? _mapper.Map<Dtos.Users.SystemSetingDto>(q_Find.University) : new Dtos.Users.SystemSetingDto();
                    res.MemberPost = q_Find.MemberPost != null ? _mapper.Map<Dtos.Users.SystemSetingDto>(q_Find.MemberPost) : new Dtos.Users.SystemSetingDto();
                    res.MemberEduction = q_Find.MemberEduction != null ? _mapper.Map<Dtos.Users.SystemSetingDto>(q_Find.MemberEduction) : new Dtos.Users.SystemSetingDto();

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
