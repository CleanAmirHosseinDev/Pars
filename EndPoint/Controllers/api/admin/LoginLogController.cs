using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
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
        private readonly IBasicInfoFacad _basicInfoFacad;
        public LoginLogController(ILogger<LoginLogController> logger, IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<LoginLogDto>>> Get_LoginLogs([FromBody]RequestLoginLogDto request)
        {
            try
            {
                return await _basicInfoFacad.GetLoginLogsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
