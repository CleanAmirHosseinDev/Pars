﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetUsers
{

    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetUsersService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<UserRolesDto> Execute(RequestUserRolesDto request)
        {
            try
            {
                UserRolesDto res = new UserRolesDto();
                res.Role = new RolesDto();
                res.User = new UsersDto();

                if (request.UserId != null && request.UserId != 0)
                {
                    var q_Find = await _context.UserRoles.Include(p => p.User).Include(p => p.Role).FirstOrDefaultAsync(p => p.UserId == request.UserId && (p.User.IsActive == request.IsActive || request.IsActive == null));

                    res = _mapper.Map<UserRolesDto>(q_Find);
                    res.User = q_Find.User != null ? _mapper.Map<UsersDto>(q_Find.User) : new UsersDto();
                    res.Role = q_Find.Role != null ? _mapper.Map<RolesDto>(q_Find.Role) : new RolesDto();

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
