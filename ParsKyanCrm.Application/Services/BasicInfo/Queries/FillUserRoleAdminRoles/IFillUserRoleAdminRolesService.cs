using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.FillUserRoleAdminRoles
{
    public interface IFillUserRoleAdminRolesService
    {
        List<NormalJsonClassDto> Execute(string roles = null);
    }
}
