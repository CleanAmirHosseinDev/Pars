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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetWorkExperience
{

    public class GetWorkExperienceService : IGetWorkExperienceService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetWorkExperienceService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<WorkExperienceDto> Execute(RequestWorkExperienceDto request)
        {
            try
            {
                WorkExperienceDto res = new WorkExperienceDto();
                res.BoardOfDirectors = new Dtos.Users.BoardOfDirectorsDto();
                res.Customer = new Dtos.Users.CustomersDto();                

                if (request.SkilsId != null && request.SkilsId != 0)
                {
                    var q_Find = await _context.WorkExperience.Include(p => p.BoardOfDirectors).Include(p => p.Customer).FirstOrDefaultAsync(p => p.SkilsId == request.SkilsId.Value && (p.IsActive == request.IsActive || request.IsActive == null) && (p.CustomerId == request.CustomerId || request.CustomerId == null) && (p.BoardOfDirectorsId == request.BoardOfDirectorsId || request.BoardOfDirectorsId == null));

                    res = _mapper.Map<WorkExperienceDto>(q_Find);
                    res.BoardOfDirectors = q_Find.BoardOfDirectors != null ? _mapper.Map<Dtos.Users.BoardOfDirectorsDto>(q_Find.BoardOfDirectors) : new Dtos.Users.BoardOfDirectorsDto();
                    res.Customer = q_Find.Customer != null ? _mapper.Map<Dtos.Users.CustomersDto>(q_Find.Customer) : new Dtos.Users.CustomersDto();                    

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
