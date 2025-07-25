﻿using Microsoft.AspNetCore.Mvc;
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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.SystemSeting })]
    public class SystemSetingController : BaseController
    {
        private readonly ILogger<SystemSetingController> _logger;
        private readonly IUserFacad _userFacad;
        public SystemSetingController(ILogger<SystemSetingController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<SystemSetingDto>>> Get_SystemSetings([FromBody] RequestSystemSetingDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetSystemSetingsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.SystemSeting_Save })]
        public async Task<SystemSetingDto> Get_SystemSeting(int? id = null)
        {
            try
            {
                return await _userFacad.GetSystemSetingService.Execute(new RequestSystemSetingDto() { SystemSetingId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.SystemSeting_Save })]
        public async Task<ResultDto<SystemSetingDto>> Save_SystemSeting([FromBody] SystemSetingDto request)
        {
            try
            {
                return await _userFacad.SaveSystemSetingService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
