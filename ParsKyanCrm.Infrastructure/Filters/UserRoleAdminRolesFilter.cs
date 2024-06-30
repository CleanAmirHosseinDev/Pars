using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure.Filters
{
    public class UserRoleAdminRolesFilter : ActionFilterAttribute
    {
        UserRoleAdminRoles[] _enumRole;

        public UserRoleAdminRoles[] Role
        {
            get { return _enumRole; }
            set { _enumRole = value; }
        }

        private List<NormalJsonClassDto> FillUserRoleAdminRolesService(string roles = null)
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

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {

                //var q_C = filterContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Menus");

                NormalJsonClassDto qSingle = Ado_NetOperation.ConvertDataTableToList<NormalJsonClassDto>(Ado_NetOperation.Select("select ur.Roles as Text,cast(ur.UserID as nvarchar(50)) as Value from UserRoles as ur where ur.UserID = " + filterContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserID").Value)).FirstOrDefault();
                NormalJsonClassDto[] listMenus = FillUserRoleAdminRolesService(qSingle.Text).Where(p => p.Selected == true).ToArray();

               // NormalJsonClassDto[] listMenus = null;

               // if (q_C != null) listMenus = JsonConvert.DeserializeObject<NormalJsonClassDto[]>(q_C.Value);

                if (rolesCheck(listMenus))
                    base.OnActionExecuting(filterContext);
                else
                {

                    filterContext.Result = new JsonResult(new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "شما مجوز انجام این عملیات را ندارید",
                        StatusCode = 301
                    });

                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new JsonResult(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "شما مجوز انجام این عملیات را ندارید",
                    StatusCode = 301
                });
                throw ex;
            }
        }

        private bool rolesCheck(NormalJsonClassDto[] listMenus)
        {
            bool rolesCheck = false;

            if (listMenus != null)
            {
                for (int i = 0; i < _enumRole.Select(p => (int)p).ToArray().Length; i++)
                {
                    if (!listMenus.Select(p => int.Parse(p.Value)).ToArray().Contains(_enumRole.Select(p => (int)p).ToArray()[i]))
                    {
                        rolesCheck = false;
                        break;
                    }
                    else rolesCheck = true;
                }
            }

            return rolesCheck;
        }
    }
}
