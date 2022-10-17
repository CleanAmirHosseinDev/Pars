using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Api.Controllers.api.admin
{
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.SystemSeting })]
    public class SystemSetingController : BaseController
    {
        private readonly ILogger<SystemSetingController> _logger;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public SystemSetingController(ILogger<SystemSetingController> logger, IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<SystemSetingDto>>> Get_SystemSetings([FromBody] RequestSystemSetingDto request)
        {
            try
            {
                return await _basicInfoFacad.GetSystemSetingsService.Execute(request);
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
                return await _basicInfoFacad.GetSystemSetingService.Execute(new RequestSystemSetingDto() { SystemSetingID = id });
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
                return await _basicInfoFacad.SaveSystemSetingService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
