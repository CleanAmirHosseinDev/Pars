using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetUserss
{

    public class GetUserssService : IGetUserssService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetUserssService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<UserRolesDto>>> Execute(RequestUserRolesDto request)
        {
            try
            {
                bool AllParskyanUser = (request.RoleId == 0 ? true : false);
                request.RoleId =(request.RoleId== 0 ?  null : request.RoleId);
                var lists = (from s in _context.UserRoles
                             where (s.User.IsActive == request.IsActive || request.IsActive == null) &&
                             (s.RoleId == request.RoleId || request.RoleId == null)
                             select s).Include(p => p.User).Include(p => p.Role).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) 
                    lists = lists.Where(p => p.User.UserName.Contains(request.Search) ||
                p.User.Mobile.Contains(request.Search) ||
                p.User.Email.Contains(request.Search)
                );

                if (AllParskyanUser)
                {
                    lists = lists.Where(p => p.RoleId != 10);
                }
                switch (request.SortOrder)
                {
                    case "UserId_D":
                        lists = lists.OrderByDescending(s => s.UserId);
                        break;
                    case "UserId_A":
                        lists = lists.OrderBy(s => s.UserId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.UserId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();
                    
                    return new ResultDto<IEnumerable<UserRolesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<UserRolesDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.UserRoles>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<UserRolesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<UserRolesDto>>(list_Res_Pageing),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = list_Res_Pageing.Rows,
                    };

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
