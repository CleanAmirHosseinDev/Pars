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

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {

                var q_C = filterContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Menus");

                NormalJsonClassDto[] listMenus = null;

                if (q_C != null) listMenus = JsonConvert.DeserializeObject<NormalJsonClassDto[]>(q_C.Value);

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
