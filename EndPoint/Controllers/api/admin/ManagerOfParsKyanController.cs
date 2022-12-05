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
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.admin
{
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ManagerOfParsKyan })]
    public class ManagerOfParsKyanController : BaseController
    {
        private readonly ILogger<ManagerOfParsKyanController> _logger;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public ManagerOfParsKyanController(ILogger<ManagerOfParsKyanController> logger, IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<ManagerOfParsKyanDto>>> Get_ManagerOfParsKyans([FromBody] RequestManagerOfParsKyanDto request)
        {
            try
            {
                return await _basicInfoFacad.GetManagerOfParsKyansService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ManagerOfParsKyan_Save })]
        public async Task<ManagerOfParsKyanDto> Get_ManagerOfParsKyan(int? id = null)
        {
            try
            {
                return await _basicInfoFacad.GetManagerOfParsKyanService.Execute(new RequestManagerOfParsKyanDto() { ManagersId = id });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ManagerOfParsKyan_Save })]
        public async Task<ResultDto<ManagerOfParsKyanDto>> Save_ManagerOfParsKyan([FromBody] ManagerOfParsKyanDto request)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
