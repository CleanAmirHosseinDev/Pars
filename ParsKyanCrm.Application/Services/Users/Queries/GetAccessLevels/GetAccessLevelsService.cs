using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
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
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetAccessLevelsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<List<NormalJsonClassDto>> Execute(int id)
        {
            try
            {
                var q_Find = await _context.UserRoles.Include(p => p.Role).FirstOrDefaultAsync(p => p.UserID == id);
                if (q_Find != null)
                {
                    return q_Find.Role.RoleTitle == "Admin" ? _basicInfoFacad.FillUserRoleAdminRolesService.Execute(q_Find.Roles) : new List<NormalJsonClassDto>();
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
