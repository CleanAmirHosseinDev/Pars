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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Companies })]
    public class CompaniesController : BaseController
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly IUserFacad _userFacad;
        public CompaniesController(ILogger<CompaniesController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<CompaniesDto>>> Get_Companiess([FromBody] RequestCompaniesDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetCompaniessService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Companies_Save })]
        public async Task<CompaniesDto> Get_Companies(int? id = null)
        {
            try
            {
                return await _userFacad.GetCompaniesService.Execute(new RequestCompaniesDto() {CompaniesId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Companies_Save })]
        public async Task<ResultDto<CompaniesDto>> Save_Companies([FromBody] CompaniesDto request)
        {
            try
            {
                return await _userFacad.SaveCompaniesService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
