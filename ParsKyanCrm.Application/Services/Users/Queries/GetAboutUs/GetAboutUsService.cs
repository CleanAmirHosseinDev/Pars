using AutoMapper;
using Microsoft.EntityFrameworkCore;

using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetAboutUs
{

    public class GetAboutUsService : IGetAboutUsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetAboutUsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AboutUsDto> Execute()
        {
            try
            {

                AboutUsDto res = new AboutUsDto();

                var q_Find = await _context.AboutUs.Include(p => p.User).FirstOrDefaultAsync();
                if (q_Find != null)
                {
                    res = _mapper.Map<AboutUsDto>(q_Find);
                    res.User = q_Find.User != null ? _mapper.Map<UsersDto>(q_Find.User) : new UsersDto();
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
