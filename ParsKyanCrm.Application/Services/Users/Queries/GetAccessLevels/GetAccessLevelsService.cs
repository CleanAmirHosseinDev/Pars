using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetAccessLevels
{

    public class GetAccessLevelsService : IGetAccessLevelsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetAccessLevelsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private List<NormalJsonClassDto> fillUserRoleAdminRolesService(string roles = null)
        {
            try
            {

                List<string> lstArr = new List<string>();

                if (!string.IsNullOrEmpty(roles)) lstArr.AddRange(roles.Split(','));

                UserRoleAdminRoles? qEnum = null;
                var q = EnumOperation<UserRoleAdminRoles>.ToSelectListByGroup(qEnum, lstArr).ToList();

                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<NormalJsonClassDto>> Execute(int id)
        {
            try
            {
                var q_Find = await _context.UserRoles.Include(p => p.Role).FirstOrDefaultAsync(p => p.UserId == id);
                if (q_Find != null)
                {
                    return q_Find.Role.RoleTitle == "Admin" ? fillUserRoleAdminRolesService(q_Find.Roles) : new List<NormalJsonClassDto>();
                }
                return new List<NormalJsonClassDto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
