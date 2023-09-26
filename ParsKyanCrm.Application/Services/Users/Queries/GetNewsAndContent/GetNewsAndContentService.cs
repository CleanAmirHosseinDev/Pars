using AutoMapper;
using Microsoft.EntityFrameworkCore;

using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetNewsAndContent
{

    public class GetNewsAndContentService : IGetNewsAndContentService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetNewsAndContentService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NewsAndContentDto> Execute(RequestNewsAndContentDto request)
        {
            try
            {
                NewsAndContentDto res = new NewsAndContentDto();
                res.User = new UsersDto();
                res.KindOfContentNavigation = new SystemSetingDto();

                if (request.ContentId != null && request.ContentId != 0)
                {
                    var q_Find = await _context.NewsAndContent.Include(p => p.User).Include(p => p.KindOfContentNavigation).FirstOrDefaultAsync(p => p.ContentId == request.ContentId.Value && (p.IsActive == request.IsActive || request.IsActive == null));
                    res = _mapper.Map<NewsAndContentDto>(q_Find);
                    res.User = q_Find.User != null ? _mapper.Map<UsersDto>(q_Find.User) : new UsersDto();
                    res.KindOfContentNavigation = q_Find.KindOfContentNavigation != null ? _mapper.Map<SystemSetingDto>(q_Find.KindOfContentNavigation) : new SystemSetingDto();
                }
                else if (request.DirectLink != null && request.DirectLink.Length > 0)
                {
                    var q_Find = await _context.NewsAndContent.Include(p => p.User).Include(p => p.KindOfContentNavigation).FirstOrDefaultAsync(p => (p.DirectLink == request.DirectLink || request.DirectLink == null) && (p.IsActive == request.IsActive || request.IsActive == null));
                    if (q_Find != null)
                    {
                        res = _mapper.Map<NewsAndContentDto>(q_Find);
                        res.User = q_Find.User != null ? _mapper.Map<UsersDto>(q_Find.User) : new UsersDto();
                        res.KindOfContentNavigation = q_Find.KindOfContentNavigation != null ? _mapper.Map<SystemSetingDto>(q_Find.KindOfContentNavigation) : new SystemSetingDto();
                    }
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
