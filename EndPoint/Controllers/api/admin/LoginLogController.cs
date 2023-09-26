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
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.admin
{
    //[UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.LoginLog })]
    public class LoginLogController : BaseController
    {
        private readonly ILogger<LoginLogController> _logger;
        private readonly IUserFacad _userFacad;
        public LoginLogController(ILogger<LoginLogController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<LoginLogDto>>> Get_LoginLogs([FromBody]RequestLoginLogDto request)
        {
            try
            {
                return await _userFacad.GetLoginLogsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
