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
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetBoardOfDirectorsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<BoardOfDirectorsDto> Execute(RequestBoardOfDirectorsDto request)
        {
            try
            {
                BoardOfDirectorsDto res = new BoardOfDirectorsDto();
                res.University = new Dtos.BasicInfo.SystemSetingDto();
                res.MemberPost = new Dtos.BasicInfo.SystemSetingDto();
                res.MemberEduction = new Dtos.BasicInfo.SystemSetingDto();

                if (request.BoardOfDirectorsId != null && request.BoardOfDirectorsId != 0)
                {
                    var q_Find = await _context.BoardOfDirectors.Include(p => p.University).Include(p => p.MemberPost).Include(p => p.MemberEduction).FirstOrDefaultAsync(p => p.BoardOfDirectorsId == request.BoardOfDirectorsId.Value && (p.IsActive == request.IsActive || request.IsActive == null) && (p.CustomerId == request.CustomerId || request.CustomerId == null));

                    res = _mapper.Map<BoardOfDirectorsDto>(q_Find);
                    res.University = q_Find.University != null ? _mapper.Map<Dtos.BasicInfo.SystemSetingDto>(q_Find.University) : new Dtos.BasicInfo.SystemSetingDto();
                    res.MemberPost = q_Find.MemberPost != null ? _mapper.Map<Dtos.BasicInfo.SystemSetingDto>(q_Find.MemberPost) : new Dtos.BasicInfo.SystemSetingDto();
                    res.MemberEduction = q_Find.MemberEduction != null ? _mapper.Map<Dtos.BasicInfo.SystemSetingDto>(q_Find.MemberEduction) : new Dtos.BasicInfo.SystemSetingDto();

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
