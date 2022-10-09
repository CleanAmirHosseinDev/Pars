using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveAccessLevels
{

    public class SaveAccessLevelsService : ISaveAccessLevelsService
    {
        public ResultDto Execute(UserRolesDto request)
        {
            try
            {

                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Users.UserRoles).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(request.Roles),request.Roles
                        }
                    }, string.Format(nameof(request.UserID) + " = '{0}' ", request.UserID));


                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ثبت سطوح دسترسی با موفقیت انجام شد"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
