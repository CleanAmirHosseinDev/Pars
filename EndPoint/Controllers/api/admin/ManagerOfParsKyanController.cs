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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ManagerOfParsKyan })]
    public class ManagerOfParsKyanController : BaseController
    {
        private readonly ILogger<ManagerOfParsKyanController> _logger;
        private readonly IUserFacad _userFacad;
        public ManagerOfParsKyanController(ILogger<ManagerOfParsKyanController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<ManagerOfParsKyanDto>>> Get_ManagerOfParsKyans([FromBody] RequestManagerOfParsKyanDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetManagerOfParsKyansService.Execute(request);
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
                return await _userFacad.GetManagerOfParsKyanService.Execute(new RequestManagerOfParsKyanDto() { ManagersId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ManagerOfParsKyan_Save })]
        public async Task<ResultDto<ManagerOfParsKyanDto>> Save_ManagerOfParsKyan([FromForm] ManagerOfParsKyanDto request)
        {
            try
            {
                request.Userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _userFacad.SaveManagerOfParsKyanService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ManagerOfParsKyan_Delete })]
        public ResultDto Delete_ManagerOfParsKyan(int id)
        {
            try
            {
                return _userFacad.DeleteManagerOfParsKyanService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
