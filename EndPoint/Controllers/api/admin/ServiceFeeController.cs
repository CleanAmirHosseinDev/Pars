using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ServiceFee })]
    public class ServiceFeeController : BaseController
    {
        private readonly ILogger<ServiceFeeController> _logger;
        private readonly IUserFacad _userFacad;
        public ServiceFeeController(ILogger<ServiceFeeController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<ServiceFeeDto>>> Get_ServiceFees([FromBody] RequestServiceFeeDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetServiceFeesService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ServiceFee_Save })]
        public async Task<ServiceFeeDto> Get_ServiceFee(int? id = null)
        {
            try
            {
                return await _userFacad.GetServiceFeeService.Execute(new RequestServiceFeeDto() { ServiceFeeId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.ServiceFee_Save })]
        public async Task<ResultDto<ServiceFeeDto>> Save_ServiceFee([FromBody] ServiceFeeDto request)
        {
            try
            {
                request.ChangeBy = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _userFacad.SaveServiceFeeService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
