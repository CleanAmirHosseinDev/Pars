﻿using Microsoft.AspNetCore.Http;
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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Contract })]
    public class ContractController : BaseController
    {
        private readonly ILogger<ContractController> _logger;
        private readonly IUserFacad _userFacad;
        public ContractController(ILogger<ContractController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<ContractDto>>> Get_Contracts([FromBody] RequestContractDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetContractsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Contract_Save })]
        public async Task<ContractDto> Get_Contract(int? id = null)
        {
            try
            {
                return await _userFacad.GetContractService.Execute(new RequestContractDto() {ContractId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Contract_Save })]
        public async Task<ResultDto<ContractDto>> Save_Contract([FromBody] ContractDto request)
        {
            try
            {
                return await _userFacad.SaveContractService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Contract_Delete })]
        public ResultDto Delete_Contract(int id)
        {
            try
            {
                return _userFacad.DeleteContractService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
