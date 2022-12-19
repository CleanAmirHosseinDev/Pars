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

namespace EndPoint.Controllers.api.superVisor
{
   
    public class ContractController : BaseController
    {
        private readonly ILogger<ContractController> _logger;
        private readonly IUserFacad _userFacad;
        public ContractController(ILogger<ContractController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }


        [Route("[action]/{id}/")]
        [HttpGet]
      //  [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Contract_Save })]
        public async Task<ContractDto> Get_Contract(int? id = null)
        {
            try
            {
                return await _userFacad.GetContractService.Execute(new RequestContractDto() {ContractId = id });
            }
            catch (Exception)
            {
                throw;
            }
        }

        

    }
}
