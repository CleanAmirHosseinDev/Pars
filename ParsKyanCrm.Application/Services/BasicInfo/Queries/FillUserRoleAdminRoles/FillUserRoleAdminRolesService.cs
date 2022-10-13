using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.FillUserRoleAdminRoles
{

    public class FillUserRoleAdminRolesService : IFillUserRoleAdminRolesService
    {
        public List<NormalJsonClassDto> Execute(string roles = null)
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
    }
}
