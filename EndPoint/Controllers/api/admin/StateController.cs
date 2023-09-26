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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.State })]
    public class StateController : BaseController
    {
        private readonly ILogger<StateController> _logger;
        private readonly IUserFacad _userFacad;
        public StateController(ILogger<StateController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<StateDto>>> Get_States([FromBody] RequestStateDto request)
        {
            try
            {
                return await _userFacad.GetStatesService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.State_Save })]
        public async Task<StateDto> Get_State(int? id = null)
        {
            try
            {
                return await _userFacad.GetStateService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.State_Save })]
        public async Task<ResultDto<StateDto>> Save_State([FromBody] StateDto request)
        {
            try
            {
                return await _userFacad.SaveStateService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
