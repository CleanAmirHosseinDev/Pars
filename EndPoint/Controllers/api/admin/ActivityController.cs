using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.admin
{
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Activity })]
    public class ActivityController : BaseController
    {
        private readonly ILogger<ActivityController> _logger;
        private readonly IUserFacad _userFacad;
        public ActivityController(ILogger<ActivityController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<ActivityDto>>> Get_Activitys([FromBody] RequestActivityDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetActivitysService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Activity_Save })]
        public async Task<ActivityDto> Get_Activity(int? id = null)
        {
            try
            {
                return await _userFacad.GetActivityService.Execute(new RequestActivityDto() { ActivityId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Activity_Save })]
        public async Task<ResultDto<ActivityDto>> Save_Activity([FromBody] ActivityDto request)
        {
            try
            {
                return await _userFacad.SaveActivityService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Activity_Delete })]
        public ResultDto Delete_Activity(int id)
        {
            try
            {
                return _userFacad.DeleteActivityService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
