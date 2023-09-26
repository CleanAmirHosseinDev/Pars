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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.City })]
    public class CityController : BaseController
    {
        private readonly ILogger<CityController> _logger;
        private readonly IUserFacad _userFacad;
        public CityController(ILogger<CityController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<CityDto>>> Get_Citys([FromBody] RequestCityDto request)
        {
            try
            {
                return await _userFacad.GetCitysService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.City_Save })]
        public async Task<CityDto> Get_City(int? id = null)
        {
            try
            {
                return await _userFacad.GetCityService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.City_Save })]
        public async Task<ResultDto<CityDto>> Save_City([FromBody] CityDto request)
        {
            try
            {
                return await _userFacad.SaveCityService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]        
        public async Task<ResultDto<IEnumerable<StateDto>>> Get_States_Combo(RequestStateDto request)
        {
            try
            {
                return (await _userFacad.GetStatesService.Execute(request));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
